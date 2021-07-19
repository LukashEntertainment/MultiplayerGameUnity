using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{

    Transform player;
    public Transform spawnPos;
    public int reset;

    public GameObject hero;

    void Start()
    {
        player = FindObjectOfType<Hero>().transform;
        reset = 0;
    }

    void Update()
    {
        if (player == null)
        {
            Debug.Log("GG");
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        if (player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, 5.5f);
        }
    }

    public void RotateCamera()
    {
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, -90f, 1);
    }
}
