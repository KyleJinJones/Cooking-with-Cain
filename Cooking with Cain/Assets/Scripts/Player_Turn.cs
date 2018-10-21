using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Turn : MonoBehaviour {
    public int atk = 10;
    public GameObject atkbutton;
    private bool attacked=false;
    public GameObject manager;
	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("Manager");
	}
	
    public void setattacked(bool b)
    {
        attacked = b;
    }
	//takes the players turn
    //should set active ui buttons
    //then recieve the result from them
    //Then calculate damage
    //Then deal damage based on the result and potentially update ui based on that
    //Note most of this is just calling functions, and it acts more as a manager for the overall player turn
	public IEnumerator turn () {
        atkbutton.SetActive(true);
        //Stalls the function here until the button has been clicked, and attacked has been set to true

        //Stalls here until the button sets attacked to true, and it proceeds to deal damage
        while (attacked == false)
        {

            yield return null;
        }

        //Deals damage to an enemy
        //Note, targeted enemy will likely be passed from ui function
        manager.GetComponent<Deal_Damage>().damage(this.gameObject,GameObject.FindGameObjectWithTag("Enemy"),atk,null);
        //turns the atkbutton off
        atkbutton.SetActive(false);
        attacked = false;
        //incrments the turncounter and stops the coroutine
        manager.GetComponent<Turn_Manager>().turncounter++;
        StopCoroutine("turn");

	}

    
}
