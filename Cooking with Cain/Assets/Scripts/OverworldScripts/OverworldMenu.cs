using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMenu : MonoBehaviour
{
    public GameObject imenu;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject settingsMenu;
    public bool menuOpen = false;
    public GameObject activeMenu;
    public AudioManager audioManager;
    //Handles menu swapping, and closing
    private void Start()
    {
        settingsMenu.SetActive(false);
        //imenu.SetActive(false);
        inventory.SetActive(false);
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Update()
    {
        this.GetComponent<AudioSource>().volume = audioManager.audioValue;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuOpen)
            {
                if (activeMenu == null) { activeMenu = settingsMenu; }
                activeMenu.SetActive(true);
                menuOpen = true;
                Time.timeScale = 0;
            }
            else
            {
                closeMenu();
            }
        }
    }

    public void openimenu()
    {
        if (!menuOpen)
        {
            imenu.SetActive(true);
            menuOpen = true;
            imenu.GetComponent<IngredientManager>().Load();
            activeMenu = imenu;
            Time.timeScale = 0;
        }
        else if (imenu.activeSelf)
        {
            imenu.SetActive(false);
            menuOpen = false;
            Time.timeScale = 1;
        }
        else
        {
            activeMenu.SetActive(false);
            imenu.SetActive(true);
            activeMenu = imenu;
            Time.timeScale = 0;
        }
    }

    public void closeMenu()
    {
        imenu.SetActive(false);
        inventory.SetActive(false);
        settingsMenu.SetActive(false);
        menuOpen = false;
        Time.timeScale = 1;
    }

    public void openinventory()
    {
        if (!menuOpen)
        {
            inventory.SetActive(true);
            menuOpen = true;
            activeMenu = inventory;
            Time.timeScale = 0;
        }
        else if (inventory.activeSelf)
        {
            inventory.SetActive(false);
            menuOpen = false;
            Time.timeScale = 1;
        }
        else
        {
            activeMenu.SetActive(false);
            inventory.SetActive(true);
            activeMenu = inventory;
            Time.timeScale = 0;
        }
    }

    public void openSettings()
    {
        if (!menuOpen)
        {
            settingsMenu.SetActive(true);
            menuOpen = true;
            activeMenu = settingsMenu;
            Time.timeScale = 0;
        }
        else if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
            menuOpen = false;
            Time.timeScale = 1;
        }
        else
        {
            activeMenu.SetActive(false);
            settingsMenu.SetActive(true);
            activeMenu = settingsMenu;
            Time.timeScale = 0;
        }
    }
}
