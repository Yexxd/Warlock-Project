using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Warlock : MonoBehaviourPun
{
    Player lasHitted;

    public float healt = 100;
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<MouseMovment>());
            Destroy(GetComponentInChildren<CameraFollow>());
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.magnitude > 11 && photonView.IsMine)
            TakeDMG(Time.deltaTime*10);
    }
    
    void GameStart()
    {
        GetComponent<MouseMovment>().canMove = true;
    }

    public void TakeDMG(Damage dmg)
    {
        if(photonView.IsMine)
        {
            if (dmg.owner != null)
                lasHitted = dmg.owner;
            TakeDMG(dmg.value);
        }
    }

    public void TakeDMG(float value)
    {
        healt -= value;
        photonView.RPC("UpdateHealt", RpcTarget.All, healt);
        if (healt <= 0)
        {
            FindObjectOfType<GameMgr>().OnKill(photonView.Owner, lasHitted);
            Muerte();
        }
    }

    [PunRPC]
    void UpdateHealt(float actualHealt)
    {
        SendMessage("UpdateHealtBar", actualHealt);
    }

    void Muerte()
    {
        PhotonNetwork.Destroy(gameObject);
    }

}
