using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Kyle Jones

public class Gold : MonoBehaviour
{
    //Used to keep track of gold, saves whenever the player's gold amt changes to prevent exploits
    public static int gold = 100;
    private TextMeshProUGUI goldtext;
    void Start()
    {
        goldtext= GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("gold"))
        {
            gold = PlayerPrefs.GetInt("gold");//PlayerPrefs.GetInt("gold")
        }
        else
        {
            PlayerPrefs.SetInt("gold", gold);
        }
        goldtext.text = string.Format("Gold:{0}", gold);
    }

    //Used to change the amt of gold available, automatically saves the amt of gold the player has whenever it changes
    public void UpdateGold(int amt)
    {
        gold += amt;
        goldtext.text = string.Format("Gold:{0}", gold);
        PlayerPrefs.SetInt("gold", gold);
    }

}
