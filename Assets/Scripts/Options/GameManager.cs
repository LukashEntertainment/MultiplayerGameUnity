using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Point[] points;
    public int[] playerScores;
    public Text[] scoreTexts;
    public GameObject winPanel;

    public static GameManager instanse;
    public static int scoreFirstPlayer;
    public static int scoreSecondPlayer;
    public bool gameIsOver;

    public PhotonView view;

    private void Awake()
    {
        instanse = this;
    }

    void Start()
    {
        points = FindObjectsOfType<Point>();
        view = GetComponent<PhotonView>();
        //playerCount = 0;
    }

    public void UpdateScore()
    {
        if (view.IsMine)
        {
            view.RPC("UpdateScoreRPC", RpcTarget.All);

            view.RPC("CheckWinnersRPC", RpcTarget.All);
        }
    }

    [PunRPC]
    public void UpdateScoreRPC()
    {
        scoreTexts[0].text = scoreFirstPlayer.ToString();
        scoreTexts[1].text = scoreSecondPlayer.ToString();
    }
    
    [PunRPC]
    public void DefaultValuesRPC()
    {
        scoreFirstPlayer = 0;
        scoreSecondPlayer = 0;
        scoreTexts[0].text = scoreFirstPlayer.ToString();
        scoreTexts[1].text = scoreSecondPlayer.ToString();
    }

    [PunRPC]
    public void CheckWinnersRPC()
    {
        if (GameManager.scoreSecondPlayer >= 5 || GameManager.scoreFirstPlayer >= 5)
        {
            if (GameManager.scoreFirstPlayer >= 5 && GameManager.scoreSecondPlayer <= 0)
            {
                gameIsOver = true;
                Debug.Log("First player WIN!");
                view.RPC("DefaultValuesRPC", RpcTarget.All);
            }
            if (GameManager.scoreSecondPlayer >= 5 && GameManager.scoreFirstPlayer <= 0)
            {
                gameIsOver = true;
                Debug.Log("Second player WIN!");
                view.RPC("DefaultValuesRPC", RpcTarget.All);
            }
            winPanel.SetActive(true);
        }
    }
}
