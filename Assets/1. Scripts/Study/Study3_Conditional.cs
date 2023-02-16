using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study3_Conditional : MonoBehaviour
{
    int hp = 100; // 체력

    // Start is called before the first frame update
    void Start()
    {
        // 체력이 0이 되면
        if (hp == 0)
        {
            // 죽음과 관련된 기능 실행
            Dead();
        }

        int a = 3;

        // a를 2로 나눈 나머지가 0이라면
        if (a % 2 == 0)
        {
            print("a는 짝수");
        }
        // 위 조건에 만족하지 못한다면 (= 나머지가 0이 아니라면)
        else
        {
            print("a는 홀수");
        }

        int score = 86;
        string grade = "";

        // 90점 이상
        if(score >= 90)
        {
            grade = "A";
        }
        // 80점 이상 90점 미만
        else if(score >= 80)
        {
            grade = "B";
        }
        // 70점 이상 80점 미만
        else if (score >= 70)
        {
            grade = "C";
        }
        // 그 외 다른 점수
        else
        {
            //...
        }

        string itemName = "총알";

        // 획득한 아이템에 따라 경우가 나눠짐
        switch (itemName)
        {
            case "빨간포션":
                // 체력 증가하는 기능
                break;

            case "총알":
                // 총알의 개수가 증가하는 기능
                break;
            
            default: // 그 외에 다른 아이템                
                break;
        }
    }

    void Dead()
    {
        // 죽었을 때 실행될 기능들
    }
}
