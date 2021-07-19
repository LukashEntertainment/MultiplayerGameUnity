using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int pointOwner;
    public SpriteRenderer[] sprites;
    public Color pointOwnerColor;
    PhotonView view;
    public Hero currentOwner;
    GameManager manager;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        manager = FindObjectOfType<GameManager>();
        pointOwner = -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (view.IsMine) { 
            if (collision.tag == "Player" && pointOwner != collision.GetComponent<Hero>().team && GameManager.instanse.gameIsOver == false)
            {
                SpriteRenderer colorRend = collision.GetComponentInChildren<SpriteRenderer>();
                Color color = colorRend.color;
                view.RPC("SwitchOwnerRPC", RpcTarget.AllBuffered, collision.GetComponent<Hero>().team, new Vector3(color.r, color.g, color.b));
            }
        }
    }

    [PunRPC]
    public void SwitchOwnerRPC(int newOwner, Vector3 color)
    {
        sprites[0].color = new Color(color.x, color.y, color.z);
        sprites[1].color = new Color(color.x, color.y, color.z);
        if (newOwner == 0)
        {
            GameManager.scoreFirstPlayer++;
            if (GameManager.scoreSecondPlayer != 0 && pointOwner == 1)
            {
                GameManager.scoreSecondPlayer--;
            }
        }

        if (newOwner == 1)
        {
            GameManager.scoreSecondPlayer++;
            if (GameManager.scoreFirstPlayer != 0 && pointOwner == 0)
            {
                GameManager.scoreFirstPlayer--;
            }
        }
        manager.UpdateScore();
        pointOwner = newOwner;
    }

}
