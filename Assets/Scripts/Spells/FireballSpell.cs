using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class FireballSpell : Spell
{
    public GameObject prefab;

    private void Awake()
    {
        prefab = Resources.Load("FireBall") as GameObject;
    }

    public override void SpellBehavior()
    {
        base.SpellBehavior();
        MouseRoutines.Screen2Plane(out Vector3 point);
        Fire(transform.position, point - transform.position);
    }

    public void Fire(Vector3 position, Vector3 direction)
    {
        direction.y = 0;
        object[] initData = {direction.normalized * 10};
        PhotonNetwork.Instantiate(prefab.name, position, Quaternion.identity,0, initData);
    }
}