using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // 포톤 관련 클래스를 사용하기 위함

public class PlaySceneManager : MonoBehaviourPunCallbacks
{
    public Transform[] playerSpawnPoints; // 플레이어 스폰 위치
    public GameObject warningTxt;         // ESC 경고 문구

    void Start()
    {   
        // 현재 방에 참여한 플레이어 인원
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        // 플레이어 인원에 따라 다른 스폰 위치에 플레이어 생성 (1명이면 0번, 2명이면 1번)
        PhotonNetwork.Instantiate("Player",
            playerSpawnPoints[playerCount - 1].position, Quaternion.identity);
    }

    private void Update()
    {
        // ESC키를 입력했다면
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(AfterEsc());
        }
    }

    // ESC키 입력 이후로 실행될 기능들
    public IEnumerator AfterEsc()
    {
        // 경고 문구 활성화
        warningTxt.SetActive(true);

        float time = 0;

        // 방에서 나가거나 3초가 지나기 전까지 반복
        while (PhotonNetwork.InRoom && time < 3)
        {
            // ESC키를 한 번 더 입력했다면
            if (time != 0 && Input.GetKeyDown(KeyCode.Escape))
            {
                // 방에서 퇴장 시도
                PhotonNetwork.LeaveRoom();

                // 반복문을 끝내서 함수 탈출
                break;
            }

            // 시간 재기
            time += Time.deltaTime;

            // 업데이트 함수가 호출될 때까지 쉬기
            yield return null;
        }

        // 경고 문구 비활성화
        warningTxt.SetActive(false);
    }

    // 엔딩 이후로 실행될 기능들
    public IEnumerator AfterEnding()
    {
        // 방에서 나가기 전까지 반복
        while (PhotonNetwork.InRoom)
        {
            // 아무 키나 입력되었다면
            if (Input.anyKeyDown)
            {
                // 방에서 퇴장 시도
                PhotonNetwork.LeaveRoom();

                // 반복문을 끝내서 함수 탈출
                break;
            }

            // 업데이트 함수가 호출될 때까지 쉬기
            yield return null;
        }
    }

    // 방 퇴장에 성공하면 호출
    public override void OnLeftRoom()
    {
        // 1. StartScene 라는 이름의 씬 불러오기 (씬 전환)
        PhotonNetwork.LoadLevel("1. StartScene");

        // 마우스 커서 보이게
        Cursor.visible = true;

        // 마우스 커서가 움직일 수 있도록 잠금 해제
        Cursor.lockState = CursorLockMode.None;
    }

    // 방에 새로운 플레이어가 입장하면 호출
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        // 방 인원 수가 최대 인원 수와 같아지면
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            // 비공개방으로 전환하고, 목록에서 숨기기
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
    }

    // 방에서 플레이어가 퇴장하면 호출
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        // 모든 PhotonView 컴포넌트를 가져와서 반복
        foreach (var player in FindObjectsOfType<PhotonView>())
        {
            // 퇴장한 플레이어의 ActorNumber와 다른 ActorNumber를 가진 플레이어 찾기
            if (player.Owner.ActorNumber != otherPlayer.ActorNumber)
            {
                // 살아남은 플레이어에게 승리시 엔딩 기능 실행
                StartCoroutine(player.GetComponent<PlayerHp>().Ending("승리"));                
            }
        }
    }
}
