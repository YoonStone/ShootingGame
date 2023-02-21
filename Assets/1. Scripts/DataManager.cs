using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private int winCount;
    public int WinCount // ������Ƽ
    {
        // WinCount ������Ƽ�� ���������� �ϸ� winCount ���� ����
        get
        {
            winCount = PlayerPrefs.GetInt("WinCount"); // �ҷ�����
            return winCount; 
        }

        // WinCount ������Ƽ�� ���� �Ҵ��ϸ� winCount ������ �Ҵ�
        set
        {
            winCount = value;
            PlayerPrefs.SetInt("WinCount", winCount); // ����
            PlayerPrefs.Save(); // �������� ��ġ�� ����
        }
    }


    // ������ ������ �� �ִ� DataManager�� �ν��Ͻ�
    public static DataManager instance;

    private void Awake()
    {
        // �ν��Ͻ��� ����ִٸ�
        if (instance == null)
        {
            // �ڱ� �ڽ� �Ҵ�
            instance = this;

            // �� ��ȯ �� �������� �ʵ��� ����
            DontDestroyOnLoad(gameObject);
        }

        // �ν��Ͻ��� ä����������, �ڱ� �ڽŰ� �ٸ��ٸ�
        else if (instance != this)
        {
            // �ڱ� �ڽ��� ������Ʈ ����
            Destroy(gameObject);
        }
    }
}
