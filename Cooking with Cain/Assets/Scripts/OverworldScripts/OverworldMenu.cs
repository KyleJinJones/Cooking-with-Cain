using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMenu : MonoBehaviour
{
    public GameObject imenu;
    public bool menuopen=false;

    private void Start()
    {
        imenu.SetActive(false);
    }

    public void openimenu()
    {
        if (!menuopen)
        {
            imenu.SetActive(true);
            menuopen = true;
            imenu.GetComponent<IngredientManager>().Load();
        }
        else if(imenu.activeSelf){
            imenu.SetActive(false);
            menuopen = false;
        }
    }

    
}
