using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
public class ConexMgr : MonoBehaviourPunCallbacks
{
    public TMP_InputField txtNick;
    public TextMeshProUGUI estado;
    public GameObject panelConex;

    private void Start()
    {
        Texture2D cursor = Resources.Load("NormalCursor") as Texture2D;
        Cursor.SetCursor(cursor, Vector3.zero, CursorMode.Auto);
    }

    public void Connect2Master()
    {
        PhotonNetwork.NickName = txtNick.text;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        panelConex.SetActive(false);
        estado.gameObject.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        estado.text = "Estado: Conectado como " + PhotonNetwork.LocalPlayer.NickName;
        Invoke(nameof(ChangeScene),1);
    }


    void ChangeScene()
    {
        SceneManager.LoadScene("Lobby");
    }

}
