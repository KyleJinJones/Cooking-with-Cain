using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeInfo.asset", menuName = "Cooking with Cain/UpgradeInfo")]
public class UpgradeInfo : ScriptableObject
{
    public enum AttributeType { STAT, INGREDIENT, GOLD, POTION, TEXT }

    // all types
    public AttributeType attributeType;
    [SerializeField] int goldcost;
    public Sprite upgradeimage;
    public string infotext;
    public string rewardname;

    // stats
    public int limit;
    public int costIncrease;
    public UpgradeInfo required;
    public Stats statsModification = Stats.zero;

    // ingredients
    public Ingredient ingredient;

    // potions
    public HPpotion.PotionType potionType;

    // gold and potions
    public int amount;

    public int totalGoldCost
    {
        get
        {
            return goldcost + boughtAmount * costIncrease;
        }
    }

    public int boughtAmount
    {
        get
        {
            UpgradeBoughtAmount a = SaveDataManager.currentData.shopBoughtStats.Find(amount => amount.upgrade == this);
            return a == null ? 0 : a.bought;
        }

        set
        {
            UpgradeBoughtAmount a = SaveDataManager.currentData.shopBoughtStats.Find(amount => amount.upgrade == this);

            if (a == null)
            {
                SaveDataManager.currentData.shopBoughtStats.Add(new UpgradeBoughtAmount(this, value));
            }
            else
            {
                a.bought++;
            }
        }
    }

    public void obtain()
    {
        switch (attributeType)
        {
            case AttributeType.STAT:
                Entity.playerStats.Add(statsModification);
                break;
            case AttributeType.INGREDIENT:
                for (int i = 0; i < 12; i++)
                {
                    if (IngredientManager.spareIngredients[i] == null)
                    {
                        IngredientManager.spareIngredients[i] = ingredient;
                        break;
                    }
                }
                break;
            case AttributeType.GOLD:
                Gold.gold += amount;
                break;
            case AttributeType.POTION:
                HPpotion.potions[(int)potionType] += amount;
                break;
            
        }
    }
}