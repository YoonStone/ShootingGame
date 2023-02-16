using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study6_Error : MonoBehaviour
{
    // Directional Light의 Light 컴포넌트를 넣어둘 변수
    public GameObject DLlight;

    // public인 할당하지 않은 변수
    public GameObject target2;

    // public이 아닌 할당하지 않은 변수
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        // DLlight 오브젝트 삭제
        Destroy(DLlight);

        /* (다른 오류가 나지 않도록 전체 주석 처리)
        // 배열의 범위를 벗어난 인덱스를 사용하면 IndexOutOfRangeException
        int[] array = { 1, 2, 3 };
        print(array[10]);

        // public인 할당하지 않은 변수를 사용하면 UnassignedReferenceException
        print(target2.name);

        // public이 아닌 할당하지 않은 변수를 사용하면 NullReferenceException
        print(target.name);
        */
    }

    // Update is called once per frame
    void Update()
    {
        // DLlight 오브젝트의 이름 출력
        print(DLlight.name);
    }
}
