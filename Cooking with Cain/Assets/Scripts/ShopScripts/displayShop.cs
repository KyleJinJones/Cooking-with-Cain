using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayShop : MonoBehaviour
{
    public static string currentShop;
    private TextMeshProUGUI shopText;

    // Update is called once per frame
    void Update()
    {
        shopText = this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        shopText.text = string.Format("Current Shop: {0}", currentShop);
    }
}
