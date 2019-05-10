using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pausemenu;
    public GameObject soundmenu;

    public GameObject[] buttons;

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
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }

    public void OpenPMenu()
    {
        if (pausemenu.activeSelf)
        {
            closePMenu();
        }
        else
        {
            pausemenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OpenSoundMenu() {
        //pausemenu.SetActive(false);
        foreach(GameObject button in buttons) {
            button.SetActive(false);
        }
        soundmenu.SetActive(true);
        print("WHATEVER");
        // Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            OpenPMenu();
        }
	}
}
