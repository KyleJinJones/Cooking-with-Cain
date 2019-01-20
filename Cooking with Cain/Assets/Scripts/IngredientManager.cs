using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles ingredient swapping, and creating a variable to pass to the battle scene for the selected ingredients
public class IngredientManager : MonoBehaviour
{
    public List<IngredientInventory> loadout;
    public List<IngredientInventory> Spare;
    public IngredientInventory ing1=null;
    public IngredientInventory ing2=null;
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    public void swapingredients()
    {
        Ingredient temp = ing1.ing;
        ing1.ing = ing2.ing;
        ing1.i.sprite = ing2.ing.sprite;
        ing2.ing = temp;
        ing2.i.sprite = temp.sprite;
        ing1 = null;
        ing2 = null;
    }
}
