using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RBSync : MonoBehaviour
{
    Rigidbody rb;
    PhotonView pv;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
    }

    private void Sync()
    {
        pv.RPC("SyncPosVel", RpcTarget.Others, transform.position, rb.velocity);
    }

    public void GotHit(Vector3 vel)
    {
        rb.velocity = vel;
        pv.RPC(nameof(SyncPosVel), RpcTarget.Others, transform.position, rb.velocity);
    }

    [PunRPC]
    void SyncPosVel(Vector3 pos, Vector3 vel)
    {
        transform.position = pos;
        rb.velocity = vel;
    }
}
