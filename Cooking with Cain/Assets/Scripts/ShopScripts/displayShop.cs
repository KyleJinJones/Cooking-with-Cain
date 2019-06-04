using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayShop : MonoBehaviour
{
    //public static string currentShop;
    public TextMeshProUGUI shopText;
    public shopScript shop;
    // Update is called once per frame
    void Update()
    {
        shopText = this.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        shopText.text = string.Format("Current Shop: {0}", shop.panels[Mathf.Abs(shop.currentPanel) % shop.panels.Count].name);
    }
}
