using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // 포톤 관련 클래스를 사용하기 위함

public class Player : MonoBehaviour
{
    public float moveSpeed;   // 이동 속도
    public float rotateSpeed; // 회전 속도
    public float jumpPower;   // 점프하는 힘

    int jumpCount; // 점프한 횟수

    Rigidbody rb;  // 플레이어의 Rigidbody 컴포넌트
    Animator anim; // 플레이어의 Animator 컴포넌트
    PhotonView pv; // 플레이어의 PhotonView 컴포넌트

    void Awake()
    {
        // 플레이어의 Rigidbody, Animator, PhotonView 컴포넌트 가져와서 저장
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();

        // 내 캐릭터일 때만 실행
        if (pv.IsMine)
        {
            // Main Camera 검색해서 가져오기
            Transform camera = Camera.main.transform;

            // Main Camera의 부모를 나로 설정
            camera.SetParent(transform);

            // 나를 기준으로 적당한 위치로 이동
            camera.localPosition = new Vector3(0, 1.2f, 0.4f);
        }
    }

    void Update()
    {
        // 내 캐릭터가 아니라면 함수 탈출하여 아래 코드 실행 불가
        if (!pv.IsMine) return;

        // 방향키 또는 WASD키 입력을 숫자로 받아서 저장
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // x축에는 h의 값을, z축에는 v의 값을 넣은 변수 생성
        Vector3 dir = new Vector3(h, 0, v);

        // 모든 방향의 속도가 동일하도록 정규화
        dir.Normalize();

        // 플레이어를 기준으로 dir의 방향 조절
        dir = transform.TransformDirection(dir);

        //// 이동할 방향에 원하는 속도 곱하기 (모든 기기에서 동일한 속도)
        //transform.position += dir * moveSpeed * Time.deltaTime;

        // 물리 작용을 이용해 이동
        rb.MovePosition(rb.position + (dir * moveSpeed * Time.deltaTime));

        // 이동하는 속도를 velocity 변수에 할당하여 애니메이션 전환
        anim.SetFloat("velocity", dir.magnitude);

        // 이동하고 있고, 점프하지 않을 때 이동 효과음 재생
        if (dir.magnitude > 0 && !anim.GetBool("isJump"))
        {
            AudioManager.instance.Audio_Walk(true);
        }
        // 이동을 멈추거나 점프하면 이동 효과음 재생 중지
        else AudioManager.instance.Audio_Walk(false);

        // 스페이스바를 누른 순간, 점프한 횟수가 2회 미만이라면
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            // 위로 순간적인 힘 발생
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            // 점프 애니메이션 실행
            anim.SetTrigger("jump");
            anim.SetBool("isJump", true);

            // 점프할 때마다 점프 횟수 증가
            jumpCount++;
        }

        // 마우스의 좌우 움직임 입력을 숫자로 받아서 저장
        float mouseMoveX = Input.GetAxis("Mouse X");

        // 마우스가 움직인 만큼 Y축 회전
        transform.Rotate(0, mouseMoveX * rotateSpeed * Time.deltaTime, 0);
    }

    // 어떤 물체와 충돌을 시작한 순간에 호출
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 물체의 태그가 "Ground"이고, 내 캐릭터일 때만
        if(collision.gameObject.tag == "Ground" && pv.IsMine)
        {
            // 점프 횟수 초기화
            jumpCount = 0;

            // 점프 애니메이션 종료
            anim.SetBool("isJump", false);
        }
    }
}
