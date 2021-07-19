using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class Hero : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float interval;
    [SerializeField] private float shotTime;

    public GameObject[] bullets;
    public GameObject[] destroyPart;
    Rigidbody2D rigidbody;

    [SerializeField] Transform shootPos;

    PhotonView view;

    //PlayerHealth health;

    public SpriteRenderer[] sprites;
    public Color[] colors;
    bool colorChanged;

    Vector2 direction;
    Vector3 screenMousePos;
    Vector3 target;
    Vector2 rotateDir;
    public int bulletSkin;
    public int team;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
        interval = Time.time + 1f;
        if (PhotonNetwork.PlayerList.Length < 2)
        {
            team = 0;
        }
        else
        {
            team = 1;
        }
    }
    
    void Update()
    {
        if (view.IsMine)
        {
            if (!colorChanged)
            {
                bulletSkin = Random.Range(0, 3);
                if (FindObjectOfType<Hero>() != null)
                {
                    if (FindObjectOfType<Hero>().GetComponentInChildren<SpriteRenderer>().color != colors[bulletSkin])
                    {
                        ChangeColor();
                    }
                    else
                    {
                        bulletSkin = Random.Range(0, 3);
                    }
                } else ChangeColor();
            }

            RotateToMouse();

             direction.x = Input.GetAxis("Horizontal");
             direction.y = Input.GetAxis("Vertical");

             if (Input.GetMouseButton(0))
             {
                 if (Time.time >= interval)
                 {
                    Shoot();
                 }
             }
        }
    }

    private void FixedUpdate()
    {
            rigidbody.MovePosition(rigidbody.position + direction * speed * Time.deltaTime);
    }

    void RotateToMouse()
    {
            screenMousePos = Input.mousePosition;
            target = Camera.main.ScreenToWorldPoint(new Vector3(screenMousePos.x, screenMousePos.y, transform.position.z));

            rotateDir = target - transform.position;
            float rotateZ = Mathf.Atan2(rotateDir.y, rotateDir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotateZ);
    }

    void Shoot()
    {
        var bullet = bullets[bulletSkin];
        bullet.GetComponent<Bullet>().jumpParticle = destroyPart[bulletSkin];
        PhotonNetwork.Instantiate(bullet.name, shootPos.position, shootPos.rotation);
        interval = Time.time + 0.3f;
    }

    public void ChangeColor()
    {
        view.RPC("ChangeColorRPC", RpcTarget.AllBuffered, new Vector3(colors[bulletSkin].r, colors[bulletSkin].g, colors[bulletSkin].b));
    }

    [PunRPC]
    public void ChangeColorRPC(Vector3 color)
    {
        sprites[0].color = new Color(color.x, color.y, color.z);
        var opacity = sprites[0].color;
        opacity.a -= 0.8f;
        sprites[1].color = opacity;
        colorChanged = true;
    }
}
