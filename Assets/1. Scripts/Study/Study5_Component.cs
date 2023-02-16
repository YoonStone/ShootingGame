using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study5_Component : MonoBehaviour
{
    // 인스펙터 창에서 관리할 수 있는 Transform 자료형의 변수
    public Transform tr;

    public Transform myTr; // 나의 Transform 컴포넌트를 넣어둘 변수
    public GameObject myG; // 나의 GameObject 컴포넌트를 넣어둘 변수
    public Light DLlight;  // Directional Light의 Light 컴포넌트를 넣어둘 변수
    public Light DLlight2;  // Light 컴포넌트를 넣어둘 변수 2

    public GameObject findCamera1; // 카메라를 검색해서 넣어둘 변수 1
    public GameObject findCamera2; // 카메라를 검색해서 넣어둘 변수 2
    public GameObject findCamera3; // 카메라를 검색해서 넣어둘 변수 3

    // Start is called before the first frame update
    void Start()
    {
        // 나의 Transform 컴포넌트를 가져와 변수에 할당
        myTr = GetComponent<Transform>();
        myTr = transform;

        // 나의 GameObject 컴포넌트를 가져와 변수에 할당
        myG = gameObject;

        // Directional Light의 Light 컴포넌트를 가져와 변수에 할당
        DLlight = tr.GetComponent<Light>();

        // Main Camera 오브젝트를 이름으로 검색해서 가져와 변수에 할당 
        findCamera1 = GameObject.Find("Main Camera");

        // Main Camera 오브젝트를 '내 자식 중에서' 이름으로 검색해서 가져온 후,
        // GameObject 컴포넌트를 가져와 변수에 할당 
        findCamera2 = transform.Find("Main Camera").gameObject;

        // Main Camera 오브젝트를 태그로 검색해서 가져와 변수에 할당 
        findCamera3 = GameObject.FindGameObjectWithTag("MainCamera");

        // Light 컴포넌트를 컴포넌트로 검색해서 가져와 변수에 할당
        DLlight2 = FindObjectOfType<Light>();
    }
}
