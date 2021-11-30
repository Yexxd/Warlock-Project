using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InmolateSpell : Spell
{
    public GameObject prefab;

    private void Awake()
    {
        prefab = Resources.Load("Explosion") as GameObject;
    }

    public override void SpellBehavior()
    {
        PhotonNetwork.Instantiate(prefab.name, caster.position, Quaternion.identity);
        caster.SendMessage("Stop");
        caster.SendMessage("SetMov",false);
        Invoke(nameof(Casteado), 2f);
    }

    void Casteado()
    {
        base.SpellBehavior();
        caster.SendMessage("TakeDMG", 5);
        caster.SendMessage("SetMov", true);
    }
}
