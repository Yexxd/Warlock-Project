using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WarlockPlayer : MonoBehaviourPun
{
    Player lastHitted;
    public float healt = 100;
    public bool canMove;

    private void Start()
    {
        lastHitted = photonView.Owner;
        GetComponent<PlayerHud>().SetNick(photonView.Owner.NickName);

        if (!photonView.IsMine)
        {
            Destroy(GetComponentInChildren<CameraFollow>().gameObject);
            GetComponent<PlayerMovment>().isMine = false;
        }
        else
        {
            PlayerData.PlayerCreated(this);
            gameObject.tag = "LocalPlayer";
        }
    }

    public void SetMov(bool v)
    {
        canMove = v;
    }

    void StartGame()
    {
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine && transform.position.magnitude > 11)
            TakeDMG(Time.deltaTime * 10);
    }

    public void TakeDMG(Damage dmg)
    {
        if(photonView.IsMine)
        {
            if (dmg.owner != null)
                lastHitted = dmg.owner;
            TakeDMG(dmg.value);
        }
    }

    public void TakeDMG(float value)
    {
        healt -= value;
        photonView.RPC("UpdateHealt", RpcTarget.All, healt);
        if (healt <= 0)
            Muerte();
    }

    [PunRPC]
    void UpdateHealt(float newHealt)
    {
        SendMessage("UpdateHealtBar", newHealt);
    }

    void Muerte()
    {
        PhotonNetwork.Destroy(gameObject);
        FindObjectOfType<GameMgr>().OnKill(photonView.Owner, lastHitted);
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