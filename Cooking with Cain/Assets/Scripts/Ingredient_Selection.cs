using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient_Selection : MonoBehaviour {
    public List<Food> selected;
    // Use this for initialization
    void Start() {

    }

  public void select (Food f){
        if (selected.Contains(f))
        {
            selected.Remove(f);
        }
        else if (selected.Count < 3)
        {
            selected.Add(f);
        }
     }

    public void clear()
    {
        selected.Clear();
    }


}
