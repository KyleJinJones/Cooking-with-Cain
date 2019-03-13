﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pausemenu;
	// Use this for initialization
	void Start () {
        pausemenu.SetActive(false);
	}

    public void closePMenu()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }

    public void OpenPMenu()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            OpenPMenu();
        }
	}
}
