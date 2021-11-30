using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{
    public string hotKey;
    public float CDBase;
    public Image cdIcon;
    protected float cdActual = 0;
    public Transform caster;


    protected virtual void Update()
    {
        if (Input.GetKeyDown(hotKey))
            if (cdActual == 0)
                SpellBehavior();
    }

    public void StartCD()
    {
        StartCoroutine(CDTimer());
    }

    IEnumerator CDTimer()
    {
        cdActual = CDBase;
        while (cdActual > 0)
        {
            cdIcon.fillAmount = cdActual / CDBase;
            cdActual -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        cdActual = 0;
    }

    public virtual void SpellBehavior()
    {
        StartCoroutine(CDTimer());
    }
}
