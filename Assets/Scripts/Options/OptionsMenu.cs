using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public Dropdown winMode, resolutionMode;
    public Slider volumeMusic;
    List<string> resolutions = new List<string>();
    float musicVolume;
    int currentResol, resolCounter = 0;

    GameObject music;

    public void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music");
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        volumeMusic.value = musicVolume;

        foreach (var i in Screen.resolutions)
        {
            resolutions.Add(i.ToString());
            if (i.width == Screen.currentResolution.width && i.height == Screen.currentResolution.height)
            {
                currentResol = resolCounter;
            }
            resolCounter++;
        }

        resolutionMode.AddOptions(resolutions);
        resolutionMode.value = currentResol;
    }

    public void ChangeVolume()
    {
        musicVolume = volumeMusic.value;
        music.GetComponent<AudioSource>().volume = musicVolume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void ChangeScreenMode()
    {
        if (winMode.value == 0)
        {
            Screen.fullScreen = true;
        }

        if (winMode.value == 1)
        {
            Screen.fullScreen = false;
        }
    }

    public void ChangeResolution()
    {
        Resolution resol = Screen.resolutions[resolutionMode.value];
        Debug.Log(resol.ToString());

        Screen.SetResolution(resol.width, resol.height, Screen.fullScreen, resol.refreshRate);
        Debug.Log(Screen.currentResolution.ToString());
    }
}
