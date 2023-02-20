using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // 포톤 관련 클래스를 사용하기 위함

public class PlayerFire : MonoBehaviour
{
    /* <총알 오브젝트를 이용한 슈팅> 
    // 총알 프리팹을 담아둘 변수
    public GameObject bulletPref;

    // 총알 발사하는 힘
    public float firePower;
    */

    // 총 효과 프리팹을 담아둘 변수
    public GameObject shootEffectPref;

    Animator anim;
    PhotonView pv;

    void Start()
    {
        anim = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();

        // 마우스 커서 안 보이게
        Cursor.visible = false;

        // 마우스 커서가 게임 화면을 벗어나지 못하도록 잠금
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        // 마우스 좌클릭을 누르는 순간, 내 캐릭터일 때만
        if (Input.GetMouseButtonDown(0) && pv.IsMine)
        {

            AudioManager.instance.Audio_Click(1); // 슈팅 효과음 재생

            /* <총알 오브젝트를 이용한 슈팅>

               //// 게임 안에 리소스 폴더에서 불러오기한 총알 프리팹의 복사본 생성
               //Instantiate(Resources.Load("Bullet"));

               // 게임 안에 총알 프리팹의 복사본 생성 (플레이어의 위치보다 1 앞에)
               // 생성 후 bullet 변수에 할당
               GameObject bullet = Instantiate(bulletPref,
                   transform.position + transform.forward, Quaternion.identity);

               // 총알 복사본이 앞으로 날아가는 순간적인 힘 발생
               bullet.GetComponent<Rigidbody>()
                   .AddForce(transform.forward * firePower, ForceMode.Impulse);
               */


            /* <Ray를 이용한 슈팅> */

            // 총 쏘는 애니메이션 실행
            anim.SetTrigger("shoot");

            // 화면 가운데에서 시작하는 Ray 생성
            Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

            // Ray에 맞은 물체를 담아둘 변수
            RaycastHit hit;

            // Ray를 발사하고, Ray에 맞은 물체는 hit에 저장, 맞은 물체가 있을 때만
            if (Physics.Raycast(ray, out hit))
            {
                // 맞은 위치에, 맞은 표면의 수직이 되는 각도로 총 효과 프리팹의 복사본 생성
                Instantiate(shootEffectPref, hit.point + hit.normal * 0.01f,
                    Quaternion.LookRotation(hit.normal), hit.transform);

                // Ray에 맞은 물체가 적이라면
                if (hit.transform.tag == "Enemy")
                {
                    // 적에게 10만큼 공격 받으라고 전달
                    hit.transform.SendMessage("Damaged", 10);
                }

                // Ray에 맞은 물체가 플레이어이라면
                if (hit.transform.tag == "Player")
                {
                    // 플레이어에게 10만큼 공격 받으라고 전달, 나의 ViewID 전달
                    hit.transform.GetComponent<PlayerHp>().Damaged(10, pv.ViewID);
                }
            }
        }
    }
}
