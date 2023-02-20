using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips; // ����� Ŭ�� �迭

    AudioSource audio_Click;  // Ŭ�� ���� ������ҽ� ������Ʈ
    AudioSource audio_Walk;   // �̵� ȿ���� ������ҽ� ������Ʈ

    // ������ ������ �� �ִ� AudioManager�� �ν��Ͻ�
    public static AudioManager instance; 

    private void Awake()
    {
        // �ν��Ͻ��� ����ִٸ�
        // = �� ���� ä���� ���� ������ ó�� �����Ѵٴ� ��
        if(instance == null)
        {
            // �ڱ� �ڽ� �Ҵ�
            instance = this;

            // �� ��ȯ �� �������� �ʵ��� ����
            DontDestroyOnLoad(gameObject);
        }

        // �ν��Ͻ��� ä����������, �ڱ� �ڽŰ� �ٸ��ٸ�
        // = ���� ������ ������Ʈ�� Ŭ����
        else if(instance != this)
        {
            // �ڱ� �ڽ��� ������Ʈ ����
            Destroy(gameObject);
        }

        // 0��: �������, 1��: Ŭ�� ȿ����, 2��: �̵� ȿ����
        audio_Click = GetComponents<AudioSource>()[1];
        audio_Walk = GetComponents<AudioSource>()[2];
    }

    // Ŭ�� ȿ���� ��� �Լ�
    public void Audio_Click(int clipNumber)
    {
        // ��Ȳ�� �°� ����� Ŭ�� ��ü �� ���
        audio_Click.clip = clips[clipNumber];
        audio_Click.Play();
    }

    // �̵� ȿ���� ��� �Լ�
    public void Audio_Walk(bool isWalking)
    {
        // �ȴ� ���̰�, ��� ���� �ƴ� ���� ���
        if(isWalking && !audio_Walk.isPlaying)
        {
            audio_Walk.Play();
        }

        // �ȴ� ���� �ƴϸ� ��� ����
        else if (!isWalking)
        {
            audio_Walk.Stop();
        }
    }
}
