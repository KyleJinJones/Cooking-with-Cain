﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles ingredient swapping, and creating a variable to pass to the battle scene for the selected ingredients
public class IngredientManager : MonoBehaviour
{
    public List<IngredientInventory> loadout;
    public List<IngredientInventory> spare;
    public IngredientInventory ing1=null;
    public IngredientInventory ing2=null;
    public static List<IngredientInventory> loadoutsaved=null;
    public static List<IngredientInventory> sparesaved=null;
    public List<Ingredient> ingredients;


    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log(loadout[0].gameObject.name);
        if (PlayerPrefs.HasKey(loadout[0].gameObject.name))
        {
            Debug.Log("Extracting");
            for (int i = 0; i < loadout.Count; i++)
            {
                int ind = PlayerPrefs.GetInt(loadout[i].gameObject.name);
                if (ind != -1)
                {
                    loadout[i].ing = ingredients[ind];
                }
                else
                {
                    loadout[i].ing = null;
                }
                Debug.Log(ind);
                loadout[i].index = ind;
               
            }

            for (int i = 0; i < spare.Count; i++)
            {
                int ind = PlayerPrefs.GetInt(spare[i].gameObject.name);
                if (ind != -1)
                {
                    spare[i].ing = ingredients[ind];
                }
                else
                {
                    spare[i].ing = null;
                }
                spare[i].index = ind;
                
            }
        }

    }
    public void swapingredients()
    {
        Ingredient temp = ing1.ing;
        ing1.ing = ing2.ing;
        if (ing2.ing != null)
        {
            ing1.i.sprite = ing2.ing.sprite;
        }
        else
        {
            ing1.i.sprite = null;
        }

        ing2.ing = temp;
        if (temp != null)
        {
            ing2.i.sprite = temp.sprite;
        }
        else
        {
            ing2.i.sprite = null;
        }
        int indtemp = ing1.index;
            ing1.index = ing2.index;
            ing2.index = indtemp;
        ing1 = null;
        ing2 = null;
    }

    public void saveing()
    {
        Debug.Log("saving");
        for(int i = 0; i < loadout.Count; i++)
        {
          
                PlayerPrefs.SetInt(loadout[i].gameObject.name, loadout[i].index);
            
        }

        for (int i = 0; i < spare.Count; i++)
        {
                Debug.Log(spare[i].index);
                PlayerPrefs.SetInt(spare[i].gameObject.name, spare[i].index);
            
        }

    }
}
