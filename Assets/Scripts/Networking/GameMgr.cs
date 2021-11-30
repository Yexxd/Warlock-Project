using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class GameMgr : MonoBehaviourPunCallbacks
{
    public List<SpawnPoint> spawnPoints;
    public TMPro.TextMeshProUGUI announceText;

    void Start()
    {
        int i = 0;
        if(PhotonNetwork.IsMasterClient)

            foreach (Player p in PhotonNetwork.PlayerList)
            {
                photonView.RPC(nameof(CrearPlayer), p, i);
                i++;
            }
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        announceText.text = "EL JUEGO COMIENZA EN \n3";
        yield return new WaitForSeconds(1);
        announceText.text = "EL JUEGO COMIENZA EN \n2";
        yield return new WaitForSeconds(1);
        announceText.text = "EL JUEGO COMIENZA EN \n1";
        yield return new WaitForSeconds(1);
        announceText.text = "GO!";
        Invoke(nameof(CleanText), 1);
        PlayerData.player.SendMessage("StartGame");
    }

   public void OnKill(Player killed, Player killer)
   {
        string msg = $"<color=blue>{killer.NickName}</color> ha matado a <color=yellow>{killed.NickName}</color>";
        photonView.RPC("Announce", RpcTarget.All, msg);
        if (FindObjectsOfType<WarlockPlayer>().Length == 1)
            photonView.RPC("RoundEnd", RpcTarget.MasterClient);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (FindObjectsOfType<WarlockPlayer>().Length == 1)
            photonView.RPC("RoundEnd", RpcTarget.MasterClient);
    }

    [PunRPC]
    IEnumerator RoundEnd()
    {
        yield return new WaitForSeconds(1);
        photonView.RPC("Announce", RpcTarget.All, "Ganador: " + FindObjectOfType<WarlockPlayer>().photonView.Owner.NickName);
        yield return new WaitForSeconds(1);
        PhotonNetwork.LoadLevel("LeaderBoardScene");
    }

    [PunRPC]
    void Announce(string str)
    {
        announceText.text = str;
        CancelInvoke(nameof(CleanText));
        Invoke(nameof(CleanText), 3);
    }

    void CleanText()
    {
        announceText.text = "";
    }

    [PunRPC]
    void CrearPlayer(int i)
    {
       PhotonNetwork.Instantiate("WarlockPlayer", spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
    }
}
