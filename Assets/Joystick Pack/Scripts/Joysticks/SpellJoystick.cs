using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SpellJoystick : Joystick
{
    public LineRenderer indicator;

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        indicator = PlayerData.player.GetComponentInChildren<LineRenderer>();
        indicator.SetPosition(1,new Vector3(Horizontal,0,Vertical).normalized*10);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<FireballSpell>().Fire(PlayerData.player.transform.position,
             PlayerData.player.transform.position + new Vector3(Horizontal,0,Vertical)*10);

        indicator.SetPosition(1,Vector3.zero);
        base.OnPointerUp(eventData);
    }
}