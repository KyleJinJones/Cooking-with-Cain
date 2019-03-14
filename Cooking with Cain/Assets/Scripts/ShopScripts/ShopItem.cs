using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI infoText;
    public UpgradeInfo upgrade;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (upgrade != null)
            UpdateInfoText();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        infoText.text = "";
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (upgrade.attributeType == UpgradeInfo.AttributeType.STAT && upgrade.limit > 0 && upgrade.boughtAmount >= upgrade.limit)
        {
            return;
        }

        if (upgrade.attributeType == UpgradeInfo.AttributeType.STAT && upgrade.required != null && !SaveDataManager.currentData.shopBoughtIngredient.Contains(upgrade.required))
        {
            return;
        }

        if (Gold.gold >= upgrade.totalGoldCost && !SaveDataManager.currentData.shopBoughtIngredient.Contains(upgrade))
        {
            upgrade.obtain();
            Gold.gold -= upgrade.totalGoldCost;

            if (upgrade.attributeType == UpgradeInfo.AttributeType.INGREDIENT)
            {
                SaveDataManager.currentData.shopBoughtIngredient.Add(upgrade);
            }
            else if (upgrade.attributeType == UpgradeInfo.AttributeType.STAT)
            {
                upgrade.boughtAmount++;
            }
        }

        UpdateInfoText();
    }

    public void UpdateInfoText()
    {
        string text = upgrade.infotext + "\n";
        
        if (SaveDataManager.currentData.shopBoughtIngredient.Contains(upgrade))
        {
            infoText.text = text + "You already have this ingredient.";
        }
        else if (upgrade.attributeType == UpgradeInfo.AttributeType.STAT && upgrade.limit > 0 && upgrade.boughtAmount >= upgrade.limit)
        {
            infoText.text = text + "Sold out";
        }
        else if (upgrade.attributeType == UpgradeInfo.AttributeType.STAT && upgrade.required != null && !SaveDataManager.currentData.shopBoughtIngredient.Contains(upgrade.required))
        {
            infoText.text = text + "You don't have this ingredient.";
        }
        else
        {
            infoText.text = text + "Cost: " + upgrade.totalGoldCost;
        }
    }
}
