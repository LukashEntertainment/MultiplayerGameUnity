using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject options;
    private void Update()
    {
        if (options != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenOrClosePanel(options);
            }
        }
    }
    public void OpenOrClosePanel(GameObject panel)
    {
        if (!panel.activeSelf) panel.SetActive(true);
        else panel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
