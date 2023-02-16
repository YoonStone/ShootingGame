using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study4_Loop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int count = 0;

        // count의 값이 10보다 작으면 반복
        while (count < 10) 
        {
            print("몬스터 소환");
            count++; // count의 값 1씩 증가
        }

        // 10번 반복
        for (int i = 0; i < 10; i++)
        {
            print("몬스터 소환2");
        }

        // 변수의 값을 감소하며 10번 반복
        for (int i = 10; i > 0; i--)
        {
            print("몬스터 소환3");
        }

        string[] stringArray = { "안녕", "하세요" };

        // stringArray 배열의 요소 개수만큼 반복
        for (int i = 0; i < stringArray.Length; i++)
        {
            stringArray[i] = "반가워요"; // 모든 요소에 값 할당
        }

        // stringArray 배열의 요소 개수만큼 반복
        foreach (var item in stringArray)
        {
            print(item); // 모든 요소의 값 출력
        }
    }
}
