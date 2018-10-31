using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Turn : MonoBehaviour {
    public GameObject atkbutton;
    private bool attacked=false;
    public GameObject manager;
    public GameObject[] playerui;
    public GameObject target;

    public bool acted = false;
	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("Manager");
        playerui = GameObject.FindGameObjectsWithTag("PlayerUI");
        target = GameObject.FindGameObjectWithTag("Enemy");

    }
	
    public void setattacked(bool b)
    { 
        attacked = b;    
    }

    //used specifically to check if three ingredients have been selected before setting attacked to true
    //Only planned to be used with the attack button
    public void check_recipe()
    {
        if (manager.GetComponent<Ingredient_Selection>().selected.Count == 3)
        {
            attacked = true;
        }
    }

    public void setui(bool b)
    {
        foreach(GameObject n in playerui) {
            n.SetActive(b);
        }
    } 

    //takes the players turn
    //should set active ui buttons
    //then recieve the result from them
    //Then calculate damage
    //Then deal damage based on the result and potentially update ui based on that
    //Note most of this is just calling functions, and it acts more as a manager for the overall player turn
    public IEnumerator turn () {
        GetComponent<Health>().UpdateTurn();
        int attack = GetComponent<Attack>().UpdateTurn();
        setui(true);
        //Stalls the function here until the button has been clicked, and attacked has been set to true

        //Stalls here until the button sets attacked to true, and it proceeds to deal damage
        while (!attacked)
        {

            yield return null;
        }

        //Deals damage to an enemy
        //Note, targeted enemy will likely be passed from ui function
       
        manager.GetComponent<Deal_Damage>().processFood(this.gameObject,target,attack, manager.GetComponent<Ingredient_Selection>().selected);
        manager.GetComponent<Ingredient_Selection>().clear();
        //turns the ui off
        setui(false);
        attacked = false;
        //incrments the turncounter and stops the coroutine
        //manager.GetComponent<Turn_Manager>().turncounter++;
        acted = true;
        StopCoroutine("turn");

	}

    
}
