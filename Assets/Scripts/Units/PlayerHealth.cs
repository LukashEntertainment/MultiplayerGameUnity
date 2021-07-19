using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    Vector2 startPos;

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        startPos = new Vector2(transform.position.x, transform.position.y);
        health = 5;
    }

    public void TakeDamage(GameObject player, GameObject particle)
    {
        health--;
        if (health <= 0)
        {
            Instantiate(particle, player.transform.position, player.transform.rotation);
            transform.position = startPos;
            health += 5;
            //Destroy(player);
        }
        Debug.Log(health);
    }

    //[PunRPC]
    //public void TakeDamageRPC()
    //{
    //    health--;
    //    if (health <= 0) Destroy(gameObject);
    //}
}
