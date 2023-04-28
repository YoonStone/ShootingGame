using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // 파일 입출력 관련 클래스를 사용하기 위함

[System.Serializable] // 직렬화
public class Data // 저장용 클래스
{
    public float hp;
    public string nickname;
    public bool[] isLevelOpen = new bool[5];
}

public class DataManagerTest : MonoBehaviour
{
    public Data data; // 저장용 클래스의 인스턴스

    string path; // 저장 경로

    // 싱글톤
    public static DataManagerTest instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // 저장 경로 설정
        path = Application.persistentDataPath + "/Data.json";

        DataManagerTest.instance.Load(); // 불러오기
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1)) // 우클릭하면
        {
            DataManagerTest.instance.Save(); // 저장
        }
    }

    // 저장
    public void Save()
    {
        // 저장용 클래스를 JSON 형식(문자열)으로 변환
        string saveData = JsonUtility.ToJson(data, true);

        // 파일로 저장
        File.WriteAllText(path, saveData);

        print("저장하기 완료");
    }

    // 불러오기
    public void Load()
    {
        // 저장된 파일이 있다면
        if (File.Exists(path))
        {
            // 저장된 파일 읽어오기
            string loadData = File.ReadAllText(path);

            // JSON 형식(문자열)으로 저장된 파일을 Data 클래스 형태로 변환
            data = JsonUtility.FromJson<Data>(loadData);

            print("불러오기 완료");
        }
        else
        {
            print("저장된 파일 없음");
        }
    }
}
