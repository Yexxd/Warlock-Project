using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textoCD;
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(CountDownLeader());
    }

    IEnumerator CountDownLeader()
    {
        for(int i=5; i>0; i--)
        {
            textoCD.text = "Reiniciando en " + i;
            yield return new WaitForSeconds(1);
        }
        Photon.Pun.PhotonNetwork.LoadLevel("Mapa1_Android");
    }
}
