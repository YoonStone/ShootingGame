using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 클래스를 사용하기 위함
using UnityEngine.AI; // AI 관련 클래스를 사용하기 위함

public class Enemy : MonoBehaviour
{
    // 적이 가질 수 있는 상태 목록
    public enum EnemyState
    {
        Idle,    // 기본
        Walk,    // 이동
        Attack,  // 공격
        Damaged, // 피격
        Dead     // 죽음
    }

    // 상태를 담아둘 변수를 만들고, 기본 상태로 시작
    public EnemyState eState = EnemyState.Idle;

    public Slider hpBar;   // 적의 체력바
    public float hp = 100; // 적의 체력

    Transform player;   // 플레이어
    NavMeshAgent agent; // NavMeshAgent 컴포넌트
    Animator anim;      // Animator 컴포넌트
    float distance;     // 플레이어와의 거리

    // 공격 받는 기능
    void Damaged(float damage)
    {
        // 공격 받은 데미지만큼 체력 감소
        hp -= damage;

        // 감소한 체력을 체력바에 표시
        hpBar.value = hp;

        agent.isStopped = true; // 이동 중단
        agent.ResetPath();      // 경로 초기화

        if (hp > 0) // 체력이 남아있다면
        {
            anim.SetTrigger("damaged");  // 피격 애니메이션 실행
            eState = EnemyState.Damaged; // 피격 상태로 전환
        }
        else // 체력이 남아있지 않다면
        {
            anim.SetTrigger("dead");  // 죽음 애니메이션 실행
            eState = EnemyState.Dead; // 죽음 상태로 전환
        }
    }

    // 피격 애니메이션이 끝나면 호출
    void DamagedEnd()
    {
        eState = EnemyState.Idle; // 기본 상태로 전환
    }

    void Start()
    {
        // Player 컴포넌트로 찾은 플레이어의 Transform 컴포넌트 가져오기
        player = FindObjectOfType<Player>().transform;

        // 나의 NavMeshAgent, Animator 컴포넌트 가져오기
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 적과 플레이어 사이의 거리 계산
        distance = Vector3.Distance(transform.position, player.position);

        // 이동에 따라 달라지는 NavMeshAgent 컴포넌트의 속도로 애니메이션 전환
        anim.SetFloat("velocity", agent.velocity.magnitude);

        // 기본, 이동, 공격 상태일 때 할 일 나누기
        switch (eState)
        {
            case EnemyState.Idle: Idle();  break;
            case EnemyState.Walk: Walk(); break;
            case EnemyState.Attack: Attack(); break;
        }
    }

    void Idle() // 기본 상태일 때 계속 할 일
    {
        // 플레이어와의 거리가 8 이하라면
        if(distance <= 8)
        {            
            eState = EnemyState.Walk; // 이동 상태로 전환
            agent.isStopped = false;  // 이동 시작
        }
    }

    void Walk() // 이동 상태일 때 계속 할 일
    {
        // 플레이어와의 거리가 8 보다 크다면
        if (distance > 8)
        {            
            eState = EnemyState.Idle; // 기본 상태로 전환
            agent.isStopped = true;   // 이동 중단
            agent.ResetPath();      // 경로 초기화
        }
        // 플레이어와의 거리가 2 이하라면
        else if (distance <= 2)
        {
            eState = EnemyState.Attack; // 공격 상태로 전환
            agent.isStopped = true;     // 이동 중단
            agent.ResetPath();          // 경로 초기화
            attackCoolTime = 1;         // 공격 상태가 되자마자 공격 시작
        }
        // 다른 상태로 전환하지 않을 때는
        else
        {
            // 플레이어의 위치를 목적지로 설정
            agent.SetDestination(player.position);
        }
    }

    float attackCoolTime; // 공격 주기를 위한 쿨타임

    void Attack() // 공격 상태일 때 계속 할 일
    {
        // 플레이어와의 거리가 2 보다 크다면
        if (distance > 2)
        {
            eState = EnemyState.Walk; // 이동 상태로 전환
            agent.isStopped = false;  // 이동 시작
        }
        // 다른 상태로 전환하지 않을 때는
        else
        {
            // 공격 상태일 때 쿨타임 계산
            attackCoolTime += Time.deltaTime;

            // 공격 쿨타임이 1초 이상이 되면
            if(attackCoolTime >= 1)
            {
                anim.SetTrigger("attack");  // 공격 애니메이션 실행
                attackCoolTime = 0;         // 다시 1초를 셀 수 있게 초기화
            }
        }
    }

    // 공격 애니메이션 중간에 호출
    void RealAttack()
    {
        // 플레이어에게 10만큼 공격 받으라고 전달
        player.SendMessage("Damaged", 10);
    }
}
