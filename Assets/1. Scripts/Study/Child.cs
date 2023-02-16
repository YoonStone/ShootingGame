using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Human // 부모 클래스 등록
{
    // Start is called before the first frame update
    void Start()
    {
        name = "김철수"; // Human 클래스의 name 변수 상속
        Sleep();        // Human 클래스의 Sleep() 상속
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
