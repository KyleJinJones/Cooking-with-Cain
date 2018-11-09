using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turn : MonoBehaviour {
    public GameObject manager;
    public List<Food> recipe;
    public bool stunned = false;
	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("Manager");
    }
	
	// Update is called once per frame
	public void turn() {
        if (stunned ==false)
        {
            GetComponent<Health>().UpdateTurn();
            int attack = GetComponent<Attack>().UpdateTurn();
            manager.GetComponent<Deal_Damage>().processFood(this.gameObject, GameObject.FindGameObjectWithTag("Player"), attack, recipe);
        }
        else
        {
            stunned = false;
        }
	}
}
