﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StartMenuScript : MonoBehaviour {


    public void gotoscene(int scene)
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(scene);
    }
    

}