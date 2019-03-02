using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMenu : MonoBehaviour
{
    public GameObject imenu;
    public GameObject inventory;
    public bool menuopen=false;
    public GameObject activemenu;
    //Handles menu swapping, and closing
    private void Start()
    {
        imenu.SetActive(false);
        inventory.SetActive(false);
    }

    public void openimenu()
    {
        if (!menuopen)
        {
            imenu.SetActive(true);
            menuopen = true;
            imenu.GetComponent<IngredientManager>().Load();
            activemenu = imenu;
            Time.timeScale = 0;
        }
        else if(imenu.activeSelf){
            imenu.SetActive(false);
            menuopen = false;
            Time.timeScale = 1;
        }
        else
        {
            activemenu.SetActive(false);
            imenu.SetActive(true);
            activemenu = imenu;
            Time.timeScale = 0;
        }
    }

    public void closeMenu()
    {
        imenu.SetActive(false);
        inventory.SetActive(false);
        menuopen = false;
        Time.timeScale = 1;
    }

  public void openinventory()
    {
        if (!menuopen)
        {
            inventory.SetActive(true);
            menuopen = true;
            activemenu = inventory;
            Time.timeScale = 0;
        }
        else if(inventory.activeSelf){
            inventory.SetActive(false);
            menuopen = false;
            Time.timeScale = 1;
        }
        else
        {
            activemenu.SetActive(false);
            inventory.SetActive(true);
            activemenu = inventory;
            Time.timeScale = 0;
        }
    }

    

    
}
