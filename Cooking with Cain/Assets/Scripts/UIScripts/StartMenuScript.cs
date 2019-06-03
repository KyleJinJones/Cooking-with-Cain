﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StartMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Intro CS");
    }

    public void Continue()
    {
        PlayerMovementFixed.spawnPosition = SaveDataManager.currentData.currentPosition;
        SceneManager.LoadScene(SaveDataManager.currentData.sceneName);
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void SceneTransition(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
