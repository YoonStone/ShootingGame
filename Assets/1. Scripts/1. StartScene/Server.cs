using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // UI 관련 클래스를 사용하기 위함
using Photon.Pun;      // 포톤 관련 클래스를 사용하기 위함
using Photon.Realtime; // OnDisconnected 함수 선언하면 자동으로 선언됨

public class Server : MonoBehaviourPunCallbacks // 포톤 관련 콜백 함수를 상속받기 위함
{
    public Button startBtn;     // 시작 버튼
    public Button exitBtn;      // 종료 버튼
    public GameObject lobby;    // 로비 화면
    public Text connectInfoTxt; // 연결 현황 텍스트
    public Text winCountTxt;    // 승리 횟수 텍스트

    void Start()
    {
        // 시작 버튼의 OnClick() 함수에 OnClickStart() 함수 연결
        startBtn.onClick.AddListener(OnClickStart);
        // 종료 버튼의 OnClick() 함수에 OnClickExit() 함수 연결
        exitBtn.onClick.AddListener(OnClickExit);

        // 시작 버튼 비활성화 상태로 시작
        startBtn.interactable = false;

        // 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();
        connectInfoTxt.text = "서버 접속 중...";

        // 승리 횟수 출력
        winCountTxt.text = "승리 횟수 : " + DataManager.instance.WinCount.ToString();
    }

    // 시작 버튼 누르면 호출
    void OnClickStart()
    {
        AudioManager.instance.Audio_Click(0); // 클릭 사운드 재생
        PhotonNetwork.JoinLobby();           // 로비 접속 시도
        connectInfoTxt.text = "로비 접속 중...";
    }

    // 종료 버튼 누르면 호출
    void OnClickExit()
    {
        AudioManager.instance.Audio_Click(0); // 클릭 사운드 재생

        // 게임 종료
#if UNITY_EDITOR // 유니티 에디터일 때
        UnityEditor.EditorApplication.isPlaying = false;
#else // 유니티 에디터가 아닐 때
        Application.Quit();
#endif
    }

    // 서버 접속에 성공하면 호출
    public override void OnConnectedToMaster()
    {
        // 시작 버튼 활성화
        startBtn.interactable = true;
        connectInfoTxt.text = "서버 접속 성공!";
    }

    // 로비 접속에 성공하면 호출
    public override void OnJoinedLobby()
    {
        // 로비 화면으로 전환
        gameObject.SetActive(false);
        lobby.SetActive(true);
        connectInfoTxt.text = "로비 접속 성공!";
    }

    // 접속에 실패하면 호출
    public override void OnDisconnected(DisconnectCause cause)
    {
        // 서버 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
        connectInfoTxt.text = "접속 실패, 서버 재접속 중...";
    }
}
