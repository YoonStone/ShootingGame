using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private int winCount;
    public int WinCount // 프로퍼티
    {
        // WinCount 프로퍼티를 가져가려고 하면 winCount 변수 제공
        get
        {
            winCount = PlayerPrefs.GetInt("WinCount"); // 불러오기
            return winCount; 
        }

        // WinCount 프로퍼티에 값을 할당하면 winCount 변수에 할당
        set
        {
            winCount = value;
            PlayerPrefs.SetInt("WinCount", winCount); // 저장
            PlayerPrefs.Save(); // 물리적인 위치에 저장
        }
    }


    // 누구나 접근할 수 있는 DataManager의 인스턴스
    public static DataManager instance;

    private void Awake()
    {
        // 인스턴스가 비어있다면
        if (instance == null)
        {
            // 자기 자신 할당
            instance = this;

            // 씬 전환 시 삭제되지 않도록 유지
            DontDestroyOnLoad(gameObject);
        }

        // 인스턴스가 채워져있지만, 자기 자신과 다르다면
        else if (instance != this)
        {
            // 자기 자신의 오브젝트 삭제
            Destroy(gameObject);
        }
    }
}
