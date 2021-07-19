using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidbody;
    public GameObject jumpParticle;
    public int damage;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
        }
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(collision.gameObject, collision.GetComponent<Hero>().destroyPart[collision.GetComponent<Hero>().bulletSkin]);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PhotonNetwork.Instantiate(jumpParticle.name, transform.position, transform.rotation);
    }
}
