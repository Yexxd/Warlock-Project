using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Fireball : MonoBehaviour, IPunInstantiateMagicCallback
{
    // Start is called before the first frame update
    bool isMine;
    Player owner;
    Rigidbody rb;

    void Awake()
    {
        Invoke(nameof(Destruir),2);
        rb = GetComponent<Rigidbody>();
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] iData = info.photonView.InstantiationData;
        rb.velocity =  (Vector3)(iData[0]);
        isMine = info.photonView.IsMine;
        owner = info.photonView.Owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody != null)
            if (!isMine & other.attachedRigidbody.gameObject.GetComponent<PhotonView>().IsMine)
            {
                other.attachedRigidbody.AddForce(rb.velocity*500);
                FindObjectOfType<GameMgr>().InflictDmg(10, other.gameObject, owner);
                GetComponent<PhotonView>().RPC("Destruir",RpcTarget.All);
            }
    }

    [PunRPC]
    void Destruir()
    {
        Destroy(gameObject);
    }
}
