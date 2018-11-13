using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turn : MonoBehaviour {
    public GameObject manager;
    public List<Food> recipe;
    public bool summonstun = false;
    public bool stunned = false;
    public bool juststunned = false;
	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("Manager");
    }
	
	// Update is called once per frame
	public void turn() {

        GetComponent<Health>().UpdateTurn();
        if (stunned ==false&&summonstun==false)
        {
            float attack = GetComponent<Attack>().UpdateTurn();
            manager.GetComponent<Deal_Damage>().processFood(this.gameObject, GameObject.FindGameObjectWithTag("Player"), attack, recipe);
        }
        else if (summonstun)
        {
            stunned = false;
            summonstun = false;
            juststunned = false;
        }
        else
        {
            stunned = false;
            juststunned = true;
        }
	}
}
