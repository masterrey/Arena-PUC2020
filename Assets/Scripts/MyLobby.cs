using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyLobby : MonoBehaviourPunCallbacks
{
    public string PlayerName;
    public GameObject roomPanel;
    public InputField ifName;
    public GameObject required;
    public GameObject namecontent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
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
        GameObject ob=PhotonNetwork.Instantiate("PlayerName", Vector3.zero, Quaternion.identity);
        //ob.transform.parent = namecontent.transform;
        
        //PhotonNetwork.LoadLevel("Level1");

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("O jogador "+newPlayer.NickName+" Entrou na Sala");
    }
}
