using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class shopChange : MonoBehaviour
{
    public bool right;
    
    private static string currentShop;
    private string nextShop;
    private TextMeshProUGUI shopText;
    //Order of Shop Screens: Upgrades, Ingredients, Potions/Items
    // Start is called before the first frame update
    void Start()
    {
        currentShop = "Upgrades";
    }

    // Update is called once per frame
    void Update() {   
        shopText = this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (right) {
            shopText.text = string.Format("Next: {0}", nextShop);
            
            if (currentShop == "Upgrades") {
                nextShop = "Ingredients";
            } else if (currentShop == "Ingredients") {
                nextShop = "Items";
            } else if (currentShop == "Items") 
                nextShop = "Upgrades";
        } else {
            shopText.text = string.Format("Previous: {0}", nextShop);
            
            if (currentShop == "Upgrades") {
                nextShop = "Items";
            } else if (currentShop == "Ingredients") {
                nextShop = "Upgrades";
            } else if (currentShop == "Items") {
                nextShop = "Ingredients";
            }
        }
        
        
        displayShop.currentShop = currentShop;
        shopScript.currentShop = currentShop;
    }

    
    void OnMouseDown() {
        if (right) {
            if (currentShop == "Upgrades") {
                currentShop = "Ingredients";
            } else if (currentShop == "Ingredients") {
                currentShop = "Items";
            } else if (currentShop == "Items") {
                currentShop = "Upgrades";
            }
        } else {
            if (currentShop == "Upgrades") {
                currentShop = "Items";
            } else if (currentShop == "Ingredients") {
                currentShop = "Upgrades";
            } else if (currentShop == "Items") {
                currentShop = "Ingredients";
            }
        }
        shopChange.currentShop = currentShop;
    }
}
