using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Maploader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Load), 1);
    }

    void Load()
    {
        PhotonNetwork.LoadLevel("Mapa1");
    }
}
