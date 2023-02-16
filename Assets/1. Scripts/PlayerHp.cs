using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; // 포톤 관련 클래스를 사용하기 위함

public class PlayerHp : MonoBehaviour
{
    public float hp = 100;     // 플레이어의 체력
    public Slider hpBar_World; // 플레이어의 체력바 (머리 위)

    Animator anim;       // 플레이어의 Animator 컴포넌트
    PhotonView pv;       // 플레이어의 PhotonView 컴포넌트
    Slider hpBar_Screen; // 플레이어의 체력바 (화면)
    Transform canvas;    // Canvas 오브젝트

    void Start()
    {
        anim = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();

        // 내 캐릭터일 때만 실행
        if (pv.IsMine)
        {
            // Canvas 검색해서 가져오기
            canvas = GameObject.Find("Canvas").transform;

            // Canvas의 부모를 나로 설정
            canvas.SetParent(transform);

            // 화면 왼쪽 상단에 있는 플레이어의 체력바 가져오기
            hpBar_Screen = canvas.GetComponentInChildren<Slider>();
        }
    }

    public void Damaged(float damage, int hitter)
    {
        // 공격 받았음을 RPC 통신으로 모두에게 전달
        pv.RPC("RPC_Damaged", RpcTarget.All, damage, hitter);
    }

    [PunRPC]
    void RPC_Damaged(float damage, int hitter)
    {
        // 공격 받은 데미지만큼 체력 감소
        hp -= damage;

        // 체력바에 체력 표시
        hpBar_World.value = hp;

        // 내 캐릭터일 때만 실행
        if (pv.IsMine)
        {
            // 화면 체력바에 체력 표시
            hpBar_Screen.value = hp;

            if (hp > 0) // 체력이 남아있다면
            {
                // 피격 애니메이션 실행
                anim.SetTrigger("damaged");
            }
            else
            {
                // 죽음 애니메이션 실행
                anim.SetTrigger("dead");

                // 카메라의 기능 중단
                GetComponentInChildren<CameraRotate>().enabled = false;

                // 패배 시 엔딩 기능 실행
                StartCoroutine(Ending("패배"));
            }
        }

        // 내 캐릭터가 아니어도 체력이 남아있지 않다면
        if (hp <= 0)
        {
            // 죽은 후에는 총에 맞지 않도록 태그 해제
            tag = "Untagged";

            // 플레이어의 기능 중단
            GetComponent<Player>().enabled = false;
            GetComponent<PlayerFire>().enabled = false;

            // 승리한 플레이어 찾아서 PlayerHp 컴포넌트 가져오기
            PlayerHp winner
                = PhotonNetwork.GetPhotonView(hitter).GetComponent<PlayerHp>();

            // 승리한 플레이어에게 승리시 엔딩 기능 실행
            StartCoroutine(winner.Ending("승리"));
        }

        /* <싱글 플레이일 때만 필요한 코드>

        // 게임 내의 모든 적의 기능 중단
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            enemy.enabled = false;
        }
        */
    }

    // 엔딩에서 실행될 기능들
    public IEnumerator Ending(string result)
    {
        // 내 캐릭터가 아니라면 함수 탈출하여 아래 코드 실행 불가
        if (!pv.IsMine) yield break;

        // 1초동안 아래 코드가 실행되지 않도록 쉬기
        yield return new WaitForSeconds(1f);

        // Canvas의 2번째 자식인 Ending 오브젝트 가져오기
        Transform ending = canvas.GetChild(2);

        // 결과 UI 출력
        ending.GetChild(0).GetComponent<Text>().text = "- " + result + " -";

        // 결과 화면 활성화
        ending.gameObject.SetActive(true);

        // 플레이어의 기능 중단
        GetComponent<Player>().enabled = false;
        GetComponent<PlayerFire>().enabled = false;
        GetComponentInChildren<CameraRotate>().enabled = false;

        // 엔딩 이후의 기능 실행
        StartCoroutine(FindObjectOfType<PlaySceneManager>().AfterEnding());
    }
}
