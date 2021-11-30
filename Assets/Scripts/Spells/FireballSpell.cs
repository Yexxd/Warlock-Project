using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class FireballSpell : Spell
{
    GameObject prefab;

    private void OnEnable()
    {
        prefab = Resources.Load("FireBall") as GameObject;
    }

    public override void SpellBehavior()
    {
        base.SpellBehavior();
        MouseRoutines.Screen2Plane(out Vector3 point);
        Fire(caster.position, point);
    }

    public void Fire(Vector3 position, Vector3 point)
    {
        caster.SendMessage("LookTo", point);
        caster.GetComponentInChildren<Animator>().SetTrigger("Fireball");
        position.y = 1;
        point.y = 1;
        PhotonNetwork.Instantiate(prefab.name, position, Quaternion.LookRotation(point - position));
    }
}