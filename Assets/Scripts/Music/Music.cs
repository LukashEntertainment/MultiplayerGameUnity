using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {

    AudioSource audio;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
    }
	
}
