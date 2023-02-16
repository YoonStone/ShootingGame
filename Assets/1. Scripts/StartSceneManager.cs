using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // StartSceneManager 클래스를 사용하기 위함

public class StartSceneManager : MonoBehaviour
{
    // 시작 버튼을 누르면 호출될 함수
    public void OnClickStart()
    {
        // 2. PlayScene 라는 이름의 씬 불러오기 (씬 전환)
        SceneManager.LoadScene("2. PlayScene");
    }
}
