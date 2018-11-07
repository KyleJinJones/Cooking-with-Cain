using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turn_Manager : MonoBehaviour {

    public GameObject[] enemies = new GameObject[3];
    public List<GameObject> queue;
    public Vector3[] enemyPositions = new Vector3[3];
    public int turnCount = 0;
    public string nextScene;

    void Start () {
        StartCoroutine(run());
    }

    IEnumerator run()
    {
        checkState();

        while (true)
        {
            turnCount++;

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
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");
        }
        
        Player_Turn playerTurn = getPlayers()[0].GetComponent<Player_Turn>();

        for (int i = 0; i < 3; i++)
        {
            if (enemies[i] == null)
            {
                if (queue.Count > 0)
                {
                    enemies[i] = queue[0];
                    enemies[i].transform.position = enemyPositions[i];
                    queue.RemoveAt(0);

                    if (playerTurn.target == null)
                    {
                        playerTurn.target = enemies[i];
                    }
                }
            }
            else
            {
                if (playerTurn.target == null)
                {
                    playerTurn.target = enemies[i];
                }
            }
        }

        if (playerTurn.target == null)
        {
            //Win state goes here.
            Debug.Log("WinBattle");
            SceneManager.LoadScene(nextScene);
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
