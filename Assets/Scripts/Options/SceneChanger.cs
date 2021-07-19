using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;
using Photon.Realtime;

public class SceneChanger : MonoBehaviourPunCallbacks
{
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Player Left");
        PhotonNetwork.LoadLevel(1);
    }


    public void Disconnect()
    {
        view.RPC("DestroyRoomRPC", RpcTarget.All);
    }

   [PunRPC]
    public void DestroyRoomRPC()
    {
        StartCoroutine(DestroyRoom());
    }

    IEnumerator DestroyRoom()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
        {
            yield return null;
        }
        PhotonNetwork.LoadLevel(1);
    }
}
