using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLoadout : MonoBehaviour
{
    // Start is called before the first frame update
    public Ingredient[] equipped;
    public List<Ingredient> ingredients;

    //KJ
    //Currently compiles a list from player prefs, will be changed to a static variable once battle transitions are available, should still work in current state
    private void Awake()
    {
        equipped = new Ingredient[6];
        for (int i = 1; i < 7; i++)
        {
            equipped[i-1]=ingredients[PlayerPrefs.GetInt("Loadout" + i)];
        }
        IngredientSelector.equipped = equipped;
        Debug.Log(IngredientSelector.equipped[4].foodName);
    }

    
}
