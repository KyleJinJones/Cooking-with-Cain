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


    // Start is called before the first frame update
   public void Start()
    {
        Debug.Log("Happens");
        if (ing != null)
        {
            i.sprite = ing.sprite;
        }
        selected.sprite = def;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
