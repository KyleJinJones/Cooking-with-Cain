using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    //Keeps track of the character's health
    public float starting_health = 100;
    private float health;
    public Text hptext;
    public Image hpbar;

	void Start () {
        health = starting_health;
        hptext.text = health.ToString();
	}
	

	//Takes an amount of damage, and updates its healthbar
	public void  damage(int amt) {
        health -= amt;
        hptext.text = health.ToString();
        hpbar.fillAmount = health / starting_health;

    }
}
