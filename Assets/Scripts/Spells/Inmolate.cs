using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Inmolate : Spell
{
    MouseMovment p;
    public GameObject prefab;

    private void Awake()
    {
        p = GetComponentInParent<MouseMovment>();
        prefab = Resources.Load("Explosion") as GameObject;
    }

    public override void SpellBehavior()
    {
        PhotonNetwork.Instantiate(prefab.name, transform.position, Quaternion.identity);
        p.canMove = false;
        p.Stop();
        Invoke(nameof(Casteado), 1.3f);
    }

    void Casteado()
    {
        base.SpellBehavior();
        GetComponentInParent<Warlock>().TakeDMG(new Damage(5));
        p.canMove = true;
    }
}
