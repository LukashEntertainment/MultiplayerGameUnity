using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    PhotonView view;
    public GameObject restartButton;
    public GameObject disconnectButton;
    public GameObject waitingText;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        if (PhotonNetwork.IsMasterClient == true)
        {
            restartButton.SetActive(true);
            disconnectButton.SetActive(true);
            waitingText.SetActive(false);
        }
        else
        {
            restartButton.SetActive(false);
            disconnectButton.SetActive(false);
            waitingText.SetActive(true);
        }
    }

    public void Restart()
    {
        view.RPC("RestartLevel", RpcTarget.All);
    }

    [PunRPC]
    public void RestartLevel()
    {
        GameManager.instanse.view.RPC("DefaultValuesRPC", RpcTarget.All);
        PhotonNetwork.DestroyAll();
        PhotonNetwork.LoadLevel(2);
    }
}
