using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomMgr : MonoBehaviourPunCallbacks
{
    public Transform layoutLista;
    public GameObject prefabPlayer, botonStart;

    void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count > 0)
            PhotonNetwork.JoinRoom("Room1");
        else
            PhotonNetwork.CreateRoom("Room1");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        PlayerListing();
        if (PhotonNetwork.IsMasterClient)
            botonStart.SetActive(true);

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerListing();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerListing();
    }

    void PlayerListing()
    {
        Player[] currPlayers = PhotonNetwork.PlayerList;
        foreach(TMPro.TextMeshProUGUI c in layoutLista.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            Destroy(c.gameObject);

        foreach (Player p in currPlayers)
            Instantiate(prefabPlayer,layoutLista).GetComponent<TMPro.TextMeshProUGUI>().text = p.NickName;
    }

    public void StartGame()
    {
        /*
        if(Application.platform != RuntimePlatform.Android)
            PhotonNetwork.LoadLevel("Mapa1");
        else*/
            PhotonNetwork.LoadLevel("Mapa1_Android");
    }
}
