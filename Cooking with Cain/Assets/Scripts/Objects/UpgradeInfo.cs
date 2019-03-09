using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeInfo.asset", menuName = "Cooking with Cain/UpgradeInfo")]
public class UpgradeInfo : ScriptableObject
{
    public enum AttributeType { STAT, INGREDIENT, GOLD, POTION }

    public AttributeType attributeType;
    public int goldcost;
    public Sprite upgradeimage;
    public string infotext;
    public string rewardname;

    public Stats statsModification = Stats.zero;

    public Ingredient ingredient;

    public HPpotion.PotionType potionType;
    public int amount;

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