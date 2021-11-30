using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UISpell : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public Image icon, cdIndicator;

    public Image LoadData(string spellName, Sprite spellIcon)
    {
        txtName.text = spellName;
        icon.sprite = spellIcon;
        return cdIndicator;
    }
}
