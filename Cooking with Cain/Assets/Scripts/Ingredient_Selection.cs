using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient_Selection : MonoBehaviour {
    public List<Food> selected;
    public PlaySound deselect;
    // Use this for initialization
    void Start() {

    }

  public void select (GameObject button){
        Food f = button.GetComponent<Food>();
        PlaySound p = button.GetComponent<PlaySound>();
        if (selected.Contains(f))
        {
            deselect.psound();
            selected.Remove(f);
        }
        else if (selected.Count < 3)
        {
            p.psound();
            selected.Add(f);
        }
     }

    public void clear()
    {
        selected.Clear();
    }


}
