using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class GameMgr : MonoBehaviourPunCallbacks
{
    public static GameObject localPlayer;
    public List<SpawnPoint> spawnPoints;
    public TMPro.TextMeshProUGUI announceText;

    void Start()
    {
        int indice = 0;
        if(PhotonNetwork.IsMasterClient)
            foreach(Player p in PhotonNetwork.PlayerList)
            {
                photonView.RPC("CrearPlayer", p, indice);
                indice++;
            }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        announceText.text = "EL JUEGO COMIENZA EN \n2";
        yield return new WaitForSeconds(1);
        announceText.text = "EL JUEGO COMIENZA EN \n1";
        yield return new WaitForSeconds(1);
        announceText.text = "GO!";
        localPlayer.SendMessage("GameStart");
        Invoke(nameof(CleanText), 1);
    }

    public void InflictDmg(float dmg, GameObject target, Player inflicter)
    {
        target.SendMessageUpwards("TakeDMG", new Damage(dmg, inflicter),SendMessageOptions.DontRequireReceiver);
    }

   public void OnKill(Player killed, Player killer)
    {
        string msg = $"<color=blue>{killer.NickName}</color> ha matado a <color=yellow>{killed.NickName}</color>";
        photonView.RPC("Announce", RpcTarget.All, msg);
        Invoke(nameof(CheckWinner), 1);
    }

    void CheckWinner()
    {
        if (FindObjectsOfType<Warlock>().Length == 1)
        {
            photonView.RPC("Announce", RpcTarget.All, "Ganador: " + FindObjectOfType<Warlock>().photonView.Owner.NickName);
            photonView.RPC("RestartRound", RpcTarget.All);
        }
    }

    [PunRPC]
    void RestartRound()
    {
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.DestroyAll();

        Invoke(nameof(ReloadScene), 3);
    }

    void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mapa1");
    }

    void CleanText()
    {
        announceText.text = "";
    }

    [PunRPC]
    void Announce(string str)
    {
        announceText.text = str;
        CancelInvoke(nameof(CleanText));
        Invoke(nameof(CleanText), 3);
    }

    [PunRPC]
    void CrearPlayer(int i)
    {
        localPlayer = PhotonNetwork.Instantiate("Player", spawnPoints[i].transform.position, Quaternion.identity);
        FindObjectOfType<PlayerData>().GiveSpells(localPlayer);
        StartCoroutine(CountDown());
    }
}

public class Damage
{
    public float value;
    public Player owner;
    public Damage(float v, Player p = null)
    {
        value = v;
        owner = p;
    }
}