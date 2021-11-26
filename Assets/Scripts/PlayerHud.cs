using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerHud : MonoBehaviourPunCallbacks
{
    public RectTransform healtBar;

    public override void OnEnable()
    {
        base.OnEnable();
        GetComponentInChildren<TextMeshProUGUI>().text = photonView.Owner.NickName;
    }

    private void UpdateHealtBar(float actualHealt)
    {
        healtBar.sizeDelta = new Vector2(actualHealt * 0.02f, 0.3f);
    }
}
