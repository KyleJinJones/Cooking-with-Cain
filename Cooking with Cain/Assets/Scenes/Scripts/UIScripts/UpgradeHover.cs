using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHover : MonoBehaviour {

    public GameObject uimanager;
    public string line;
    public string attribute;
    
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnMouseEnter () {
        //uimanager.GetComponent<UpgradeUI>().Displayui(line);
	}
    void OnMouseDown()
    {
        if (attribute == "food")
        {
            GetComponent<EnableIngredient>().Enableing();
        }
        else
        {
            uimanager.GetComponent<Upgrade>().ChangeAttribute(attribute);
            uimanager.GetComponent<Upgrade>().ChangeScene();
        }
        
    }
}
