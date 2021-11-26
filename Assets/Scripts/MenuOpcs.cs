using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MenuOpcs : MonoBehaviour
{
    public void Back2Menu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}
