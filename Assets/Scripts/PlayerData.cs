using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public int kills, gold;

    public List<SpellData> ownedSpells;
    public GameObject UIspellPrefab;
    public Transform spellsPanel;

    static PlayerData single;
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

    public void GiveSpells(GameObject warlock)
    {
        spellsPanel = GameObject.Find("SpellsPanel").transform;

        GameObject spellBook = warlock.transform.GetChild(0).gameObject;
        foreach (SpellData spell in ownedSpells)
        {
            Image icon = Instantiate(UIspellPrefab, spellsPanel).GetComponent<UISpell>().LoadData(spell.name, spell.icon);
            Spell s = spellBook.AddComponent(Type.GetType(spell.script.name)) as Spell;
            s.cdIcon = icon;
            s.CDBase = spell.CDBase;
            s.hotKey = spell.hotkey;
        }
    }
}
