using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips; // 오디오 클립 배열

    AudioSource audio_Click;  // 클릭 관련 오디오소스 컴포넌트
    AudioSource audio_Walk;   // 이동 효과음 오디오소스 컴포넌트

    // 누구나 접근할 수 있는 AudioManager의 인스턴스
    public static AudioManager instance; 

    private void Awake()
    {
        // 인스턴스가 비어있다면
        // = 한 번도 채워진 적이 없으니 처음 실행한다는 뜻
        if(instance == null)
        {
            // 자기 자신 할당
            instance = this;

            // 씬 전환 시 삭제되지 않도록 유지
            DontDestroyOnLoad(gameObject);
        }

        // 인스턴스가 채워져있지만, 자기 자신과 다르다면
        // = 새로 생성된 오브젝트의 클래스
        else if(instance != this)
        {
            // 자기 자신의 오브젝트 삭제
            Destroy(gameObject);
        }

        // 0번: 배경음악, 1번: 클릭 효과음, 2번: 이동 효과음
        audio_Click = GetComponents<AudioSource>()[1];
        audio_Walk = GetComponents<AudioSource>()[2];
    }

    // 클릭 효과음 재생 함수
    public void Audio_Click(int clipNumber)
    {
        // 상황에 맞게 오디오 클립 교체 후 재생
        audio_Click.clip = clips[clipNumber];
        audio_Click.Play();
    }

    // 이동 효과음 재생 함수
    public void Audio_Walk(bool isWalking)
    {
        // 걷는 중이고, 재생 중이 아닐 때만 재생
        if(isWalking && !audio_Walk.isPlaying)
        {
            audio_Walk.Play();
        }

        // 걷는 중이 아니면 재생 중지
        else if (!isWalking)
        {
            audio_Walk.Stop();
        }
    }
}
