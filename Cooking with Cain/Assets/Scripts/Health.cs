using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    //Keeps track of the character's health
    public int health = 100;
    public Text healthbar;

	void Start () {
        healthbar.text = health.ToString();
	}
	

	//Takes an amount of damage, and updates its healthbar
	public void  damage(int amt) {
        health -= amt;
        healthbar.text = health.ToString();
    }
}
