using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Manager : MonoBehaviour {

    public GameObject[] enemies=null;
    public GameObject[] players=null;
    //turncounter controls whose turn it is
    public int turncounter=0;
    private int delay =0;
	// Use this for initialization
	void Start () {
        //if enemies have not been manually added, searches the scene for them
        //All enemies should have the tage Enemy
		if (enemies.Length==0)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        //If Players were not manually instantiated, searches the scene for them
        //All Player controlled characters should have the tag Player
        if (players.Length==0)
        {
            players= GameObject.FindGameObjectsWithTag("Player");
        }

    }

    // Update is called once per frame
    void Update() {

        if (turncounter < players.Length)
        {
            //starts a coroutine at playerturn which increments turncounter once it is finished and terminates itself
            players[turncounter].GetComponent<Player_Turn>().StartCoroutine("turn");
        }
        else if(turncounter >= enemies.Length+players.Length){
            //resets the turncounter to start over from the first players turn
            turncounter = 0;
        }
        else
        {
            //Has a slight delay to show that enemies attack individually, and that player is unable to attack during their turn
            //Then calls an enemies turn and increments the turncounter
            if (delay > 100)
            {
                enemies[turncounter - players.Length].GetComponent<Enemy_Turn>().turn();
                turncounter++;
                delay = 0;
            }
            delay++;
        }

    }
}
