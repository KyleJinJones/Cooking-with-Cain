using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turn : MonoBehaviour {
    public GameObject manager;
    public 
	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("Manager");
    }
	
	// Update is called once per frame
	public void turn() {
        GetComponent<Health>().UpdateTurn();
        int attack = GetComponent<Attack>().UpdateTurn();
        manager.GetComponent<Deal_Damage>().damage(this.gameObject, GameObject.FindGameObjectWithTag("Player"), attack, false, 0);
	}
}
