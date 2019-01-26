using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLoadout : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Ingredient> equipped;
    public List<Ingredient> ingredients;


    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 1; i < 7; i++)
        {
            equipped.Add(ingredients[PlayerPrefs.GetInt("Loadout" + i)]);
        }
    }

    
}
