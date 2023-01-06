using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class PlayerData : MonoBehaviour
{
    public int kills, gold;

    public List<SpellData> ownedSpells;
    public GameObject UIspellPrefab;
    public Transform spellsPanel;
    static PlayerData single;
    public static WarlockPlayer player;

    private void Awake()
    {
        if (single)
        {
            Destroy(gameObject);
            return;
        }
        single = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void PlayerCreated(WarlockPlayer p)
    {
        player = p;
        /*
        if(Application.platform != RuntimePlatform.Android)
            single.GiveSpells();
        */
    }

    public void GiveSpells()
    {
        spellsPanel = GameObject.Find("SpellsPanel").transform;
        foreach (Spell spell in GetComponents<Spell>())
            Destroy(spell);

        foreach (SpellData spell in ownedSpells)
        {
            Image icon = Instantiate(UIspellPrefab, spellsPanel).GetComponent<UISpell>().LoadData(spell.name, spell.icon);
            Spell s =  gameObject.AddComponent(Type.GetType(spell.script.name)) as Spell;
            s.cdIcon = icon;
            s.CDBase = spell.CDBase;
            s.hotKey = spell.hotkey;
            s.caster = player.transform;
        }
    }
}
