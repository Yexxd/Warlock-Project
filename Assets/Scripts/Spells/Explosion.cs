using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(Explotar), 2);
    }

    void Explotar()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<SphereCollider>().enabled = true;
        Destroy(gameObject,0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<PhotonView>().IsMine)
            return;
        if(other.CompareTag("LocalPlayer"))
        {
            float d = 4 - Vector3.Distance(other.transform.position, transform.position);

            other.SendMessage("GotHit", d * (other.transform.position - transform.position));
            other.SendMessage("TakeDMG",new Damage(15, GetComponent<PhotonView>().Owner));
        }
    }
}
