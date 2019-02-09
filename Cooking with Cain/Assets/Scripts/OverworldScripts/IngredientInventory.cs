using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInventory : MonoBehaviour
{
    //handles each single ingredient instance in the inventory

    [SerializeField] private IngredientManager igm;
    public Ingredient ing=null;
    public Image i;
    public Image selected;
    public Sprite select;
    public Sprite def;
    public int index=-1;

    //Sets the image for the ingredient
   public void Start()
    {
        
        if (ing != null)
        {
            i.sprite = ing.sprite;
        }
        selected.sprite = def;

    }

    //Handles selections, and indicators, as well as the logic behind how ingredients are allowed to be selected
    public void Selected()
    {
        if (igm.ing1 ==null)
        {
            selected.sprite = select;
            igm.ing1 = this;
        }
        else if (igm.ing2 == null)
        {
            igm.ing2 = this;
        }
        else if (igm.ing1 == this)
        {
            selected.sprite = def;
            igm.ing1 = null;
        }
        else
        {
            selected.sprite = select;
            igm.ing1.selected.sprite = def;
            igm.ing1 = this;
        }

        if (igm.ing1 != null&&igm.ing2!=null)
        {
            igm.ing1.selected.sprite = def;
            igm.swapingredients();
        }
    }
}
