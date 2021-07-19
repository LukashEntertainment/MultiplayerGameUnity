using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP;
    public GameObject particle, blood, bullet;
    [SerializeField] Transform shootPos;
    public float interval;
    public PhotonView view;

    public void GetDamage(int dmg)
    {
        view.RPC("GetDamageRPC", RpcTarget.AllBuffered, dmg);
    }

    [PunRPC]
    public void GetDamageRPC(int dmg)
    {
        HP-=dmg;
        if (HP == 0)
        {
            PhotonNetwork.Instantiate(particle.name, transform.position, transform.rotation);
            PhotonNetwork.Instantiate(blood.name, transform.position, transform.rotation);
            GameObject.FindGameObjectWithTag("CameraHolder").GetComponent<Animator>().SetTrigger("Shake");
            //PhotonNetwork.Destroy(gameObject);
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        PhotonNetwork.Instantiate(bullet.name, shootPos.position, shootPos.rotation);
        interval = Time.time + 0.3f;
    }
}
