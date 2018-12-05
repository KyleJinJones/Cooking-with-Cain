using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour {

    public GameObject ui;
    public Text uitext;
    
	// Use this for initialization
	void Start () {
        ui.SetActive(false);
	}

    public void Displayui(string s)
    {
        ui.SetActive(true);
        uitext.text = s;
    }
    
}
