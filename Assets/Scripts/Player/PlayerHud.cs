using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    public RectTransform healtBar;

    public void SetNick(string nick)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = nick;
    }

    private void UpdateHealtBar(float actualHealt)
    {
        healtBar.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        healtBar.sizeDelta = new Vector2(actualHealt * 0.02f, 0.3f);
        Invoke(nameof(SetGreen), 0.1f);
    }

    void SetGreen()
    {
        healtBar.GetComponent<UnityEngine.UI.Image>().color = Color.green;
    }
}
