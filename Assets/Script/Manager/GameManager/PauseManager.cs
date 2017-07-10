using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    bool paused = false;
    [SerializeField]
    GameObject pauseUI;

    void Start()
    {
        pauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            paused = !paused;
        }
        if (paused)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (!paused)
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);           
        }
    }

    public void Reprendre()
    {
        paused = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Quitter()
    {
        Application.Quit();
    }

}

