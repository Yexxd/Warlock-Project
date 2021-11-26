using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "SpellScritpable")]
public class SpellData : ScriptableObject
{
    public string spellName, hotkey;
    public float CDBase;
    public bool pasive;
    public Sprite icon;
    public Texture2D cursor;
    public TextAsset script;
}

