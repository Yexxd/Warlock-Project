using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Connect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room1",new Photon.Realtime.RoomOptions(), PhotonNetwork.CurrentLobby);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("WarlockPlayer", Vector3.zero, Quaternion.identity);
    }
}
