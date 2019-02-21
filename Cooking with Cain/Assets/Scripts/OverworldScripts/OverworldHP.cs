using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OverworldHP : MonoBehaviour
{
    public TextMeshProUGUI hptext;
    public Image hpimage;
    public float currenthp=100;
    public float maxhp=100;

    // Loads Playerhp from prefs or sets it if it has not been set, and updates the ui display
    void Start()
    {
        hptext.GetComponentInChildren<TextMeshProUGUI>();

        /*if (PlayerPrefs.HasKey("MaxHP"))
        {
            maxhp = PlayerPrefs.GetFloat("MaxHP");
        }
        else
        {
            PlayerPrefs.SetFloat("MaxHP", 100);
        }
        if (PlayerPrefs.HasKey("CurrentHP"))
        {
            currenthp = PlayerPrefs.GetFloat("CurrentHP");
        }
        else
        {
            PlayerPrefs.SetFloat("CurrentHP", 100);
        }*/
        maxhp = Entity.playerStats.maxHealth;
        currenthp = Entity.playerStats.health;
        UpdateDisplay();
    }

    // Used to changehp, autosaves player hp to avoid exploits
    public void ChangeHP(float val)
    {
        currenthp += val;
        if (currenthp > maxhp)
        {
            currenthp = maxhp;
        }
        if (currenthp < 0)
        {
            currenthp = 0;
        }
        UpdateDisplay();
        Entity.playerStats.health = currenthp;
    }

    //Updates the displayed HP in the UI
    void UpdateDisplay()
    {
        hptext.text = currenthp.ToString();
        hpimage.fillAmount = (currenthp / maxhp);
    }
}
