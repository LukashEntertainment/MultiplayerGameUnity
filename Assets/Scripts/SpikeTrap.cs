using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public GameObject spike;
    public Transform[] spikeSpawnPoints;
    public float interval;
    float spawnTime;

    void Start()
    {
        spawnTime = Time.time + interval;
        foreach (Transform pos in spikeSpawnPoints)
        {
            Instantiate(spike, pos.position, pos.rotation, transform);
        }
    }

    void Update()
    {
        if (spawnTime <= Time.time)
        {
            foreach (Transform pos in spikeSpawnPoints)
            {
                Instantiate(spike, pos.position, pos.rotation, transform);
            }
            spawnTime = Time.time + interval;
        }
    }
}
