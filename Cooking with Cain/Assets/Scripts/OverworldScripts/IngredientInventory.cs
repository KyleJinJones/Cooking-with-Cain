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
    //public int index=-1;

    //Sets the image for the ingredient
   public void Start()
    {
        
        if (ing == null)
        {
            i.sprite = null;
        }
        else
        {
            i.sprite = ing.sprite;
        }
        selected.sprite = def;
        
    }

    private void Update()
    {
        UpdateTooltip();
        if (ing != null)
        {
            i.sprite = ing.sprite;
        }
    }

    void UpdateTooltip()
    {
        if (ing != null)
        {

            float attack = Entity.playerStats.attack;

            string tooltip = ing.foodName;

            switch (ing.damageType)
            {
                case Ingredient.DamageType.flat:
                    tooltip += string.Format("\n+{0} damage", Mathf.RoundToInt(attack * ing.multiplier));
                    break;
                case Ingredient.DamageType.range:
                    tooltip += string.Format("\n+{0}~{1} damage", Mathf.RoundToInt(attack * ing.multiplierMin), Mathf.RoundToInt(attack * ing.multiplierMax));
                    break;
            }

            Stats stats = Entity.playerStats;

            switch (ing.attribute)
            {
                case Ingredient.Attribute.burn:
                    tooltip += string.Format("\nEffect: {0}% Burn", Mathf.RoundToInt(stats.burn * 100));
                    break;
                case Ingredient.Attribute.splash:
                    tooltip += string.Format("\nEffect: {0}% Splash", Mathf.RoundToInt(stats.splash * 100));
                    break;
                case Ingredient.Attribute.leech:
                    tooltip += string.Format("\nEffect: {0}% Lifesteal", Mathf.RoundToInt(stats.lifesteal * 100));
                    break;
                case Ingredient.Attribute.atkup:
                    tooltip += string.Format("\nEffect: {0}% Attack boost", Mathf.RoundToInt(stats.atkboost * 100));
                    break;
                case Ingredient.Attribute.atkdown:
                    tooltip += string.Format("\nEffect: {0}% Attack debuff", Mathf.RoundToInt(stats.atkdebuff * 100));
                    break;
                case Ingredient.Attribute.stun:
                    tooltip += string.Format("\nEffect: {0}% Stun chance", Mathf.RoundToInt(stats.stun * 100));
                    break;
                case Ingredient.Attribute.defup:
                    tooltip += string.Format("\nEffect: {0}% Defense boost", Mathf.RoundToInt(stats.defboost * 100));
                    break;
                case Ingredient.Attribute.defdown:
                    tooltip += string.Format("\nEffect: {0}% Defense debuff", Mathf.RoundToInt(stats.defdebuff * 100));
                    break;
                case Ingredient.Attribute.reflect:
                    tooltip += string.Format("\nEffect: {0}% Reflect", Mathf.RoundToInt(stats.reflect * 100));
                    break;
                case Ingredient.Attribute.cleanse:
                    tooltip += string.Format("\nEffect: Debuff cleanse");
                    break;
                case Ingredient.Attribute.selfdmg:
                    tooltip += string.Format("\nEffect: {0}% Self damage", Mathf.RoundToInt(stats.selfdmg * 100));
                    break;
                case Ingredient.Attribute.miss:
                    tooltip += string.Format("\nEffect: {0}% Miss chance", Mathf.RoundToInt(stats.miss * 100));
                    break;
            }

            TooltipText tooltipText = gameObject.GetComponent<TooltipText>();
            if (tooltipText == null)
                tooltipText = gameObject.AddComponent<TooltipText>();

            tooltipText.text = tooltip;
        }
    }

    //Handles selections, and indicators, as well as the logic behind how ingredients are allowed to be selected
    // Modified so you can only swap if there is an ingredient. CT
    public void Selected()
    {
        if (ing != null)
        {
            if (igm.ing1 == null)
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

            if (igm.ing1 != null && igm.ing2 != null)
            {
                if (igm.ing1.ing != null && igm.ing2.ing != null)
                {
                    igm.ing1.selected.sprite = def;
                    igm.swapingredients();
                }
            }
        }
    }
}
