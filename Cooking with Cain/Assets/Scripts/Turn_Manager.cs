﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Manager : MonoBehaviour {

    public GameObject[] enemies = new GameObject[3];
    public List<GameObject> queue = new List<GameObject>();

    void Start () {
        StartCoroutine(run());
    }

    IEnumerator run()
    {
        while (true)
        {
            foreach (GameObject player in getPlayers())
            {
                if (player != null && player.GetComponent<Health>().health > 0)
                {
                    player.GetComponent<Player_Turn>().acted = false;
                    player.GetComponent<Player_Turn>().StartCoroutine("turn");
                    yield return new WaitUntil(() => player.GetComponent<Player_Turn>().acted);
                    for (int i = 0; i < 100; i++)
                        yield return null;
                }

                checkState();
            }

            foreach (GameObject enemy in getEnemies())
            {
                if (enemy != null && enemy.GetComponent<Health>().health > 0)
                {
                    enemy.GetComponent<Enemy_Turn>().turn();
                    for (int i = 0; i < 100; i++)
                        yield return null;
                }

                checkState();
            }
        }
    }

    public void checkState()
    {
        if (getPlayers().Length == 0)
        {
            // lose state goes here
        }

        // checks to see if any enemies are still alive
        bool cont = false;

        for (int i = 0; i < 3; i++)
        {
            if (enemies[i] == null)
            {
                if (queue.Count > 0)
                {
                    enemies[i] = queue[0];
                    queue.RemoveAt(0);
                    cont = true;
                }
            }
            else
            {
                cont = true;
            }
        }

        if (cont)
        {
            // win state goes here
        }
    }

    public GameObject[] getEnemies()
    {
        return enemies;
    }

    public GameObject[] getPlayers()
    {
        return GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update() {

        

    }
}
