using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : Enemy
{
    public float speed, maxDist;

    Vector2 startPos;
    GameObject hero;
    float distance;
    Vector3 target;
    Vector2 rotateDir;

    void Start()
    {
        //hero = GameObject.FindObjectOfType<PlayerController>();
        hero = GameObject.FindGameObjectWithTag("Player");
        startPos = new Vector2(transform.position.x, transform.position.y);
        interval = Time.time + 1f;
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (hero != null)
        {
            distance = Vector3.Distance(hero.transform.position, gameObject.transform.position);
            if (distance <= maxDist)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(hero.transform.position.x - 3, hero.transform.position.y - 3, hero.transform.position.z), speed * Time.deltaTime);
                target = new Vector3(hero.transform.position.x, hero.transform.position.y, transform.position.z);

                rotateDir = target - transform.position;
                float rotateZ = Mathf.Atan2(rotateDir.y, rotateDir.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, rotateZ);

                if (Time.time >= interval)
                {
                    Shoot();
                }
            }
            else
            {
                //transform.position = transform.position;
                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                target = new Vector3(startPos.x, startPos.y, transform.position.z);

                rotateDir = target - transform.position;
                float rotateZ = Mathf.Atan2(rotateDir.y, rotateDir.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, rotateZ);
            }
        }
        else
        {
            //hero = GameObject.FindObjectOfType<PlayerController>();
            hero = GameObject.FindGameObjectWithTag("Player");
            transform.position = startPos;
        }
    }
}
