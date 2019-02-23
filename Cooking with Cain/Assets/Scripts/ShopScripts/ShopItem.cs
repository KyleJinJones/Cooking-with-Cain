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
            infoText.text = upgrade.infotext + "\n" + "Cost: " + upgrade.goldcost;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        infoText.text = "";
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (Gold.gold >= upgrade.goldcost && !SaveDataManager.currentData.shopBought.Contains(upgrade))
        {
            upgrade.obtain();
            Gold.gold -= upgrade.goldcost;

            if (upgrade.attributeType == UpgradeInfo.AttributeType.INGREDIENT)
            {
                SaveDataManager.currentData.shopBought.Add(upgrade);
            }
        }
    }
}
