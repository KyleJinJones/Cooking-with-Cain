using System.Collections;
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

    // Saving and loading spare ingredients. CT
    public static Ingredient[] spareIngredients
    {
        get
        {
            return SaveDataManager.currentData.spare;
        }
    }

    public static Ingredient[] currentIngredients
    {
        get
        {
            return SaveDataManager.currentData.equipped;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        Load();
    }

    public void Load()
    {
        for (int i = 0; i < IngredientSelector.equipped.Length; i++)
        {
            if (IngredientSelector.equipped[i] != null)
            {
                Debug.Log(IngredientSelector.equipped[i].name);
                loadout[i].ing = IngredientSelector.equipped[i];
                loadout[i].Start();
            }
            //loadout[i].index = i;
        }

        for (int i = 0; i < spareIngredients.Length; i++)
        {
            spare[i].ing = spareIngredients[i];
            spare[i].Start();
            //spare[i].index = i;
        }

        /*if (PlayerPrefs.HasKey(loadout[0].gameObject.name))
           {

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
                loadout[i].Start();
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
                   spare[i].Start();
           }

       }
       else
       {
           saveing();
       }*/

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
        /*int indtemp = ing1.index;
            ing1.index = ing2.index;
            ing2.index = indtemp;*/
        ing1 = null;
        ing2 = null;
        saveing();
    }

    public void saveing()
    {
        for(int i = 0; i < loadout.Count; i++)
        {
            //PlayerPrefs.SetInt(loadout[i].gameObject.name, loadout[i].index);
            IngredientSelector.equipped[i] = loadout[i].ing;
        }

        for (int i = 0; i < spare.Count; i++)
        {

            //PlayerPrefs.SetInt(spare[i].gameObject.name, spare[i].index);
            spareIngredients[i] = spare[i].ing;
        }

    }
}
