using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Fireball2 : Spell
{
    public Texture2D cursor;
    public GameObject prefab;
    bool casting;
    Plane plane = new Plane(Vector3.up, 0);

    public override void SpellBehavior()
    {
        Cursor.SetCursor(cursor, Vector2.one*64, CursorMode.Auto);
        casting = true;
    }

    protected override void Update()
    {
        base.Update();
        if(casting)
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 spawnPos = GameMgr.localPlayer.transform.position;
                    Vector3 targetPos = ray.GetPoint(distance);
                    Debug.Log(spawnPos.ToString());
                    casting = false;
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    view.RPC("Fire", RpcTarget.AllViaServer, spawnPos + Vector3.up, targetPos - spawnPos);
                    base.StartCD();
                }
            }
    }

    [PunRPC]
    public void Fire(Vector3 position, Vector3 direction)
    {
        GameObject bullet;
        /** Use this if you want to fire one bullet at a time **/
        bullet = Instantiate(prefab, position, Quaternion.identity);
    }
}
