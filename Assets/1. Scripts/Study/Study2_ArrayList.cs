using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study2_ArrayList : MonoBehaviour
{
    // 배열 선언 방법 1. 선언하자마자 바로 할당
    public int[] intArray = { 10, 5, -6, 48 };

    // 배열 선언 방법 2. 공간의 개수만 설정
    public int[] intArray2 = new int[3];

    // 리스트 선언
    public List<int> intList = new List<int>();

    // 리스트 선언하자마자 바로 할당
    public List<int> intList2 = new List<int>() { 48, 3, -2 };

    // Start is called before the first frame update
    void Start()
    {
        // intArray 배열의 2번 요소의 값 출력
        print(intArray[2]);

        // intArray 배열의 0번 요소에 값 할당
        intArray[0] = 1;

        // intList2 리스트의 1번 요소에 값 할당
        intList2[1] = 5;

        // intList 리스트에 순서대로 값 추가
        intList.Add(-10);
        intList.Add(48);

        // intList 리스트 중간에 값 추가 (1번 요소로 5 추가)
        intList.Insert(1, 5);

        // intList 리스트에서 -10이라는 값 삭제
        intList.Remove(-10);

        // intList 리스트에서 1번 요소 삭제
        intList.RemoveAt(1);

        // intList 리스트에 5라는 값이 들어있다면
        if (intList.Contains(5))
        {
            // intList 리스트에 5라는 값이 몇 번 인덱스로 들어있는지 출력
            print(intList.IndexOf(5));
        }
    }
}
