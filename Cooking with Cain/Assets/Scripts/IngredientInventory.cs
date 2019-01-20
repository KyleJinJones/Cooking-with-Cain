using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInventory : MonoBehaviour
{
    //handles each single ingredient instance in the inventory

    [SerializeField] private IngredientManager igm;
    public Ingredient ing;
    public Image i;
    
    
    // Start is called before the first frame update
    void Start()
    {
        i.sprite = ing.sprite;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selected()
    {
        if (igm.ing1 ==null)
        {
            igm.ing1 = this;
        }
        else if (igm.ing2 == null)
        {
            igm.ing2 = this;
        }
        else if (igm.ing1 == this)
        {
            igm.ing1 = null;
        }
        else if (igm.ing2 == this)
        {
            igm.ing2 = null;
        }
        else
        {
            igm.ing1 = this;
        }

        if (igm.ing1 != null&&igm.ing2!=null)
        {
            igm.swapingredients();
        }
    }
}
