using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Fireball : MonoBehaviour
{
    Rigidbody rb;
    PhotonView pv;

    void OnEnable()
    {
        Invoke(nameof(Destruir),2);
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward*10;
        pv = GetComponent<PhotonView>();
    }

    void Destruir()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pv.Owner == other.GetComponent<PhotonView>()?.Owner)
            return;

        if (!pv.IsMine && other.gameObject.CompareTag("LocalPlayer"))
        {
            PlayerData.player.TakeDMG(new Damage(15, pv.Owner));
            other.SendMessage("GotHit",rb.velocity);
        }

        Destroy(gameObject);
    }
}
