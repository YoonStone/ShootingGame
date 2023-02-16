using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    // 1. 속성 (변수)
    public string name; // 이름
    private int age;    // 나이
    float height;       // 키

    // 2. 기능 (함수)
    private void Eat()
    {
        print("먹기");
    }

    public void Sleep()
    {
        print("잠자기");
    }
}
