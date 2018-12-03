using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Turn_Manager : MonoBehaviour {

    public GameObject[] enemies = new GameObject[3];
    public Image[] enemyHpBars = new Image[3];
    public Text[] enemyHpText = new Text[3];
    public Text[] enemyNames = new Text[3];
    public Text[] enemyDamageIndicators = new Text[3];
    public Text enemyRemaining;
    public List<GameObject> queue;
    public Vector3[] enemyPositions = new Vector3[3];
    public int turnCount = 0;
    public string nextScene;
    public GameObject targetDisplayObject;

    void Start () {
        StartCoroutine(run());
    }

    void Update()
    {
        displayHealth();
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

                if (checkState())
                    yield break;
            }

            foreach (GameObject enemy in getEnemies())
            {
                if (enemy != null && enemy.GetComponent<Health>().health > 0)
                {
                    if (enemy.GetComponent<Enemy_Turn>().summonstun)
                    {
                        enemy.GetComponent<Enemy_Turn>().summonstun = false;
                    }
                    else
                    {
                        enemy.GetComponent<Enemy_Turn>().turn();
                        for (int i = 0; i < 100; i++)
                            yield return null;
                    }
                }

                if (checkState())
                    yield break;
            }
        }
    }

    public void displayHealth()
    {
        if (getPlayers().Length > 0)
        {
            Player_Turn turn = getPlayers()[0].GetComponent<Player_Turn>();
            targetDisplayObject.SetActive(turn.target != null && turn.target.GetComponent<Health>().health != 0);
            if (turn.target != null)
            {
                targetDisplayObject.transform.SetPositionAndRotation(turn.target.transform.position, Quaternion.identity);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (enemies[i] == null)
            {
                enemyHpBars[i].fillAmount /= 5;
                enemyHpText[i].text = "";
                enemyNames[i].text = "";
            }
            else
            {
                Health health = enemies[i].GetComponent<Health>();
                enemyHpText[i].text = health.health.ToString();
                enemyHpBars[i].fillAmount = health.renderhealth / health.starting_health;
                enemyNames[i].text = enemies[i].GetComponent<Enemy_Turn>().enemyName;
            }
        }
    }

    public int getEnemyRemaining()
    {
        int remain = queue.Count;

        for (int i = 0; i < 3; i++)
        {
            if (enemies[i] != null)
            {
                remain++;
            }
        }

        return remain;
    }

    // returns true if the level ends
    public bool checkState()
    {
        if (getPlayers().Length == 0)
        {
            // lose state goes here
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");
            return true;
        }

        Player_Turn playerTurn = getPlayers()[0].GetComponent<Player_Turn>();

        for (int i = 0; i < 3; i++)
        {
            if (enemies[i] == null)
            {
                if (queue.Count > 0)
                {
                    enemies[i] = queue[0];
                    enemies[i].GetComponent<Health>().damageIndicator = enemyDamageIndicators[i];
                    if (turnCount != 0)
                    {
                        enemies[i].GetComponent<Enemy_Turn>().summonstun = true;
                    }
                    StartCoroutine(enemyAppear(enemies[i], enemyPositions[i]));
                    //enemies[i].transform.position = enemyPositions[i];

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

        enemyRemaining.text = "Enemies remaining: " + getEnemyRemaining();

        if (getEnemyRemaining() == 0)
        {
            //Win state goes here.
            Debug.Log("WinBattle");
            SceneManager.LoadScene(nextScene);
            return true;
        }

        return false;
    }

    IEnumerator enemyAppear(GameObject enemy, Vector3 position)
    {
        for (int i = 9; i >= 0; i--)
        {
            enemy.transform.position = position + new Vector3(0, i);
            yield return null;
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
}
