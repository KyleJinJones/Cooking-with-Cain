﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HPpotion : MonoBehaviour
{
    public float healamt=25;
    public OverworldHP hp;
    public int amtowned = 0;
    public TextMeshProUGUI ownedtxt;
    //KJ
    //Onstart, checks how many of the hp potion the player has in prefs and loads it, otherwise sets it to a default of 0
    private void Start()
    {
        if (PlayerPrefs.HasKey(this.name))
        {
            amtowned = PlayerPrefs.GetInt(this.name);
        }
        else
        {
            PlayerPrefs.SetInt(this.name, amtowned);
        }
        ownedtxt = GetComponentInChildren<TextMeshProUGUI>();
        UpdateAmtOwned();
    }

    //Adds to player hp when used, but cannot be used at max hp. Auto saves the number of potions remaining
    public void OnUse()
    {
        if (amtowned > 0&& hp.currenthp<hp.maxhp)
        {
            hp.ChangeHP(healamt);
            amtowned -= 1;
            UpdateAmtOwned();
        }
    }

    private void UpdateAmtOwned()
    {
        ownedtxt.text = amtowned.ToString();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(this.name, amtowned);
    }
}
