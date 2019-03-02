using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLoadout : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Ingredient> ingredients;

    //KJ
    //Currently compiles a list from player prefs, will be changed to a static variable once battle transitions are available, should still work in current state
    // Modified so that it provides default ingredients for testing purposes. CT
    private void Awake()
    {
        /*for (int i = 0; i < 6; i++)
        {
            if (IngredientSelector.equipped[i] == null)
                IngredientSelector.equipped[i] = ingredients[i];
        }*/
    }

    
}
