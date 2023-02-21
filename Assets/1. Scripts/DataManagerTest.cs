using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // ���� ����� ���� Ŭ������ ����ϱ� ����

[System.Serializable] // ����ȭ
public class Data // ����� Ŭ����
{
    public float hp;
    public string nickname;
    public bool[] isLevelOpen = new bool[5];
}

public class DataManagerTest : MonoBehaviour
{
    public Data data; // ����� Ŭ������ �ν��Ͻ�

    string path; // ���� ���

    // �̱���
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

        // ���� ��� ����
        path = Application.persistentDataPath + "/Data.json";

        DataManagerTest.instance.Load(); // �ҷ�����
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1)) // ��Ŭ���ϸ�
        {
            DataManagerTest.instance.Save(); // ����
        }
    }

    // ����
    public void Save()
    {
        // ����� Ŭ������ JSON ����(���ڿ�)���� ��ȯ
        string saveData = JsonUtility.ToJson(data, true);

        // ���Ϸ� ����
        File.WriteAllText(path, saveData);

        print("�����ϱ� �Ϸ�");
    }

    // �ҷ�����
    public void Load()
    {
        // ����� ������ �ִٸ�
        if (File.Exists(path))
        {
            // ����� ���� �о����
            string loadData = File.ReadAllText(path);

            // JSON ����(���ڿ�)���� ����� ������ Data Ŭ���� ���·� ��ȯ
            data = JsonUtility.FromJson<Data>(loadData);

            print("�ҷ����� �Ϸ�");
        }
        else
        {
            print("����� ���� ����");
        }
    }
}
