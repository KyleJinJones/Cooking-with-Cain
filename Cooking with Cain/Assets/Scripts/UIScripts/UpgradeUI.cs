using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour {

    public GameObject ui;
    public TextMeshProUGUI uitext;
    public TextMeshProUGUI cost;
    public Gold g;
    
	// Use this for initialization
	void Start () {
        //ui.SetActive(false);
	}

    public void Displayui(string attrinfo, int goldcost)
    {
        ui.SetActive(true);
        uitext.text = attrinfo;
        cost.text = string.Format("Cost:{0}", goldcost);
    }

    public void paycost(int goldamt)
    {
        g.UpdateGold(-goldamt);
    }

    
    
}
