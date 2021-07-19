using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    int i = 0;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        if (PhotonNetwork.PlayerList.Length < 2)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[0].position, Quaternion.identity);
        } else PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[1].position, Quaternion.identity);
    }
}
