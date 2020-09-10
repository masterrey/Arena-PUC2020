﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MyLobby : MonoBehaviourPunCallbacks
{
    public string PlayerName;
    public GameObject roomPanel;
    public InputField ifName;
    public GameObject required;
    public PlayerName[] playersNames;
    public enum GameType
    {
        Tank,FPS
    }
    public GameType gameType;
    
    public void PlayGame()
    {
        if (ifName.text.Length > 0)
        {
            PlayerName = ifName.text;
            PhotonNetwork.LocalPlayer.NickName = PlayerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            required.SetActive(true);
        }

    }

    public void JoinRoomFPS()
    {
        gameType = GameType.FPS;
        RoomOptions rOp = new RoomOptions();
        rOp.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom("FPSRoom01",rOp,TypedLobby.Default,null);
        //PhotonNetwork.JoinRandomRoom();
    }
     public void JoinRoomTank()
    {
        gameType = GameType.Tank;
        RoomOptions rOp = new RoomOptions();
        rOp.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom("TankRoom01",rOp, TypedLobby.Default, null);
    }

    public override void OnConnectedToMaster()
    {
        roomPanel.SetActive(true);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Não encontrou sala, Criando uma...");
        string roomName = "Sala00";
        RoomOptions rOp = new RoomOptions();
        rOp.MaxPlayers = 20;
        PhotonNetwork.CreateRoom(roomName, rOp);

    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("PlayerName", Vector3.zero, Quaternion.identity);

        InvokeRepeating("CheckAllReady", 1, 1);
        //PhotonNetwork.LoadLevel("Level1");

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("O jogador "+newPlayer.NickName+" Entrou na Sala");
       
        
    }


    void CheckAllReady() 
    {
        print("Checando...");
        playersNames = FindObjectsOfType<PlayerName>();
        bool allready = true;
        if (playersNames.Length > 1)
        {
            allready = playersNames.All(param => param.ready);
           
            if (allready)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false; //fecha a sala pra ninguem entrar

                switch (gameType)
                {
                    case GameType.Tank:
                        PhotonNetwork.LoadLevel("Level1");
                        break;
                    case GameType.FPS:
                        PhotonNetwork.LoadLevel("Level2");
                        break;
                }
            }
        }
    }
}
