using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Manager : MonoBehaviour {

    public GameObject[] enemies=null;
    public GameObject[] players=null;
    //turncounter controls whose turn it is
    public int turncounter=0;
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

        StartCoroutine(run());
    }

    IEnumerator run()
    {
        while (true)
        {
            foreach (GameObject player in players)
            {
                player.GetComponent<Player_Turn>().acted = false;
                player.GetComponent<Player_Turn>().StartCoroutine("turn");
                yield return new WaitUntil(() => player.GetComponent<Player_Turn>().acted);
                for (int i = 0; i < 100; i++)
                    yield return null;
            }

            foreach (GameObject enemy in enemies)
            {
                if (enemy.GetComponent<Health>().health > 0)
                {
                    enemy.GetComponent<Enemy_Turn>().turn();
                    for (int i = 0; i < 100; i++)
                        yield return null;
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {

        

    }
}
