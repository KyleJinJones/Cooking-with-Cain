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
    public Text enemyRemaining;
    public List<GameObject> queue;
    public Vector3[] enemyPositions = new Vector3[3];
    public int turnCount = 0;
    public string nextScene;

    void Start () {
        StartCoroutine(run());
    }

    void FixedUpdate()
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

    public void displayHealth()
    {
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
        }
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

    // Update is called once per frame
    void Update() {

        

    }
}
