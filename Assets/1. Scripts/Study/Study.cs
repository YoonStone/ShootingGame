using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study : MonoBehaviour
{
    enum Week // 요일을 표현할 새로운 자료형
    {
        Mon = 1, // 월요일
        Tue,     // 화요일
        Wed,     // 수요일
        Thu,     // 목요일
        Fri,     // 금요일
        Sat,     // 토요일
        Sun      // 일요일
    }

    enum State // 캐릭터의 상태
    {
        Idle,    // 기본
        Walk,    // 이동
        Attack,  // 공격
        Damaged, // 피격
        Dead     // 죽음
    }

    // Start is called before the first frame update
    void Start()
    {
        print("안녕하세요~!");

        print(10 + 5); // 덧셈
        print(10 - 5); // 뺄셈
        print(10 * 5); // 곱셈
        print(10 / 5); // 나눗셈
        print(10 % 5); // 나머지

        print(3 + 5 * 2 + (5 - 2)); // 괄호 > 곱셈 및 나눗셈 > 덧셈 및 뺄셈  

        print(5 / 10); // 정수와 정수의 연산 = 정수 

        print(0.1f + 1.9f); // 정수와 정수의 연산 = 정수 
        print(0.2f * 3.0f);

        print(5 + 10f); // 정수와 실수의 연산 = 실수


        print('가');

        print("안녕" + "하세요~!"); // 문자열 사이 + 부호


        print(3 < 5); // 결과가 '참'인 비교식
        print(3 > 5); // 결과가 '거짓'인 비교식

        print(3 < 5 && 5 < 10); // 두 비교식이 모두 true일 때만 true
        print(3 < 5 || 5 > 10); // 두 비교식 중 하나만 true여도 true
        print(3 == 5); // 두 값이 같은지 비교
        print(3 != 5); // 두 값이 같지 않은지 비교
        print(!true); // 논리 부정


        int speed = 100; // int 자료형의 값을 담을 speed라는 이름의 변수 선언 후 100 할당

        speed = 100; // speed 변수에 정수 100 할당
        print(speed); // speed 변수의 값 출력


        int add = Addition(5); // add 변수 선언 후, Addition() 함수의 반환값 할당
        print(add);            // 반환값 출력

        print(Addition(1, 2)); // 함수를 호출하며 인수 2개 전달 + 반환값 출력

        print(Addition()); // 인수 없이 함수 호출 + 반환값 출력

        Addition(1f); // 함수를 호출하는 것까지만 가능


        print(Week.Sat); // 열거형으로 만들어진 일주일 자료형 중, 토요일 출력
    }

    // Update is called once per frame
    void Update()
    {

    }

    int Addition(int x)
    {
        return x + 1;
    }

    int Addition(int x, int y)
    {
        return x + y; // 매개변수끼리 덧셈
    }

    int Addition()
    {
        // 함수 안에서 변수 선언 후 할당
        int x = 2;
        int y = 3;
        return x + y;
    }

    void Addition(float x)
    {
        // 반환할 수 없으니 함수 안에서 출력까지 처리
        print(x + 1);
    }
}
