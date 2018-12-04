using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StartMenuScript : MonoBehaviour {


    public void gotoscene(int scene)
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(scene);
    }

    public void contgame()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("level"));
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
    

}
