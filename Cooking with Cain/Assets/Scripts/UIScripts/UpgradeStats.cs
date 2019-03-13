using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStats : MonoBehaviour
{

    public GameObject uimanager;
    public UpgradeInfo upgrade;
    private Image i;
    //f = food (ingredient)
    //i == item
    //a == attribute (upgrade)
    
    

    void Start()
    {
        i = this.GetComponent<Image>();
        i.sprite = upgrade.upgradeimage;
    }

    // Update is called once per frame
    void OnMouseEnter()
    {
        uimanager.GetComponent<UpgradeUI>().Displayui(upgrade.infotext,upgrade.totalGoldCost);
    }

    void OnMouseDown()
    {
        /*if (upgrade.attributetype == "f"&&Gold.gold>=upgrade.goldcost)
        {
            ///Change picture to default and unclickable
            if (!upgrade.inInventory) {
                uimanager.GetComponent<UpgradeUI>().paycost(upgrade.goldcost);
                uimanager.GetComponent<AddIng>().adding((int)upgrade.upgradeamt);
                upgrade.inInventory = true;
            }
            
        }
        else if(Gold.gold >= upgrade.goldcost&& upgrade.attributetype == "i")
        {
            uimanager.GetComponent<UpgradeUI>().paycost(upgrade.goldcost);
            PlayerPrefs.SetInt(upgrade.attributename, PlayerPrefs.GetInt(upgrade.attributename) + (int)upgrade.upgradeamt);
        }
        else if (Gold.gold >= upgrade.goldcost)
        {
            uimanager.GetComponent<UpgradeUI>().paycost(upgrade.goldcost);
            PlayerPrefs.SetFloat(upgrade.attributename, (PlayerPrefs.GetFloat(upgrade.attributename) + upgrade.upgradeamt));
        }*/

    }

    public void SwitchUpgrade(UpgradeInfo u)
    {
        upgrade = u;
        i.sprite = u.upgradeimage;
    }
}
