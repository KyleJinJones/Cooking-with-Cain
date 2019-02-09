using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStats : MonoBehaviour
{

    public GameObject uimanager;
    public UpgradeInfo upgrade;
    private Image i;
    
    

    void Start()
    {
        i = this.GetComponent<Image>();
        i.sprite = upgrade.upgradeimage;
    }

    // Update is called once per frame
    void OnMouseEnter()
    {
        uimanager.GetComponent<UpgradeUI>().Displayui(upgrade.infotext,upgrade.goldcost);
    }
    void OnMouseDown()
    {
        if (upgrade.attributetype == "f"&&Gold.gold>=upgrade.goldcost)
        {
            uimanager.GetComponent<UpgradeUI>().paycost(upgrade.goldcost);
            GetComponent<EnableIngredient>().Enableing();
        }
        else
        {
            PlayerPrefs.SetFloat(upgrade.attributename, PlayerPrefs.GetFloat(upgrade.attributename) + upgrade.upgradeamt);
        }

    }
}
