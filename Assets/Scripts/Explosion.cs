using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Explosion : MonoBehaviour, IPunInstantiateMagicCallback
{
    public SpriteRenderer spr;
    bool isMine;
    Player owner;
    float a = 0;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        isMine = info.photonView.IsMine;
        owner = info.photonView.Owner;
    }

    private void Update()
    {
        Color c = new Color(1, 1, 1, a);
        spr.color = c;

        if (a < 1)
            a += Time.deltaTime * 0.8f;
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<SphereCollider>().enabled = true;
            Destroy(gameObject,0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
            if(!isMine & other.attachedRigidbody.gameObject.GetComponent<PhotonView>().IsMine)
            {
                other.attachedRigidbody.AddExplosionForce(7500, transform.position, 10);
            }
    }
}
