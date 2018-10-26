﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    //Keeps track of the character's health
    public float starting_health = 100;
    public float health;
    public Text hptext;
    public Image hpbar;
    public int burnDuration;

	void Start () {
        health = starting_health;
        hptext.text = health.ToString();
	}
	
    public void UpdateTurn()
    {
        if (burnDuration > 0)
        {
            damage(Mathf.RoundToInt(starting_health * 0.07f));
            burnDuration--;
        }
    }

	//Takes an amount of damage, and updates its healthbar
	public void  damage(int amt) {
        health -= amt;
        hptext.text = health.ToString();
        hpbar.fillAmount = health / starting_health;

    }

    public void heal(int amt)
    {
        health += amt;

        if (health > starting_health)
        {
            health = starting_health;
        }

        hptext.text = health.ToString();
        hpbar.fillAmount = health / starting_health;

    }
}
