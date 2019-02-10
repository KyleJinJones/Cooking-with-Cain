using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AttackManager))]
public class EntityManager : MonoBehaviour
{
    Entity player;
    public Entity[] enemies = new Entity[3];
    public bool playerTurn { get; private set; }

    AttackManager attackManager;

    public Image targetIndicator;

    public List<Entity> queue = new List<Entity>();

    public Entity targeted = null;

    public GameObject ingredientMenu;

    public Vector3[] positions = new Vector3[3];

    public TextMeshProUGUI enemyRemaining;

    int turnCount = 0;

    // Scene to transition to if the battle is won. CT
    public static string overworldScene;

    void Awake()
    {
        playerTurn = false;
        attackManager = GetComponent<AttackManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
    }

    void Start()
    {
        StartCoroutine(Run());
    }

    void Update()
    {
        enemyRemaining.text = string.Format("Enemy Remaining: {0}", GetEnemyRemaining());
    }

    public void PlayerAction(Ingredient[] ingredients)
    {
        if (playerTurn)
        {
            //ingredientMenu.SetActive(false);
            targetIndicator.gameObject.SetActive(false);
            attackManager.ProcessAttack(player, targeted, enemies, ingredients);
            playerTurn = false;
        }
    }

    public void StartPlayerTurn()
    {
        playerTurn = true;

        for (int i = 0; i < 3; i++)
        {
            if (enemies[i] == null)
            {
                if (queue.Count > 0)
                {
                    enemies[i] = queue[0];
                    Entity enemy = enemies[i];

                    Button button = enemy.gameObject.AddComponent<Button>();
                    button.onClick.AddListener(() => {
                        targeted = enemy;
                        targetIndicator.transform.position = enemy.transform.position;
                    });

                    ResultText.lines.Add(string.Format("{0} spawns", enemy.entityName));
                    StartCoroutine(EnemyAppear(enemy.gameObject, positions[i]));
                    queue.RemoveAt(0);

                    if (targeted == null || targeted.stats.health == 0)
                    {
                        targeted = enemy;
                    }
                }
            }
            else
            {
                if (targeted == null || targeted.stats.health == 0)
                {
                    targeted = enemies[i];
                }
            }
        }
    }

    IEnumerator EnemyAppear(GameObject enemy, Vector3 position)
    {
        for (int i = 9; i >= 0; i--)
        {
            enemy.transform.localPosition = position + new Vector3(0, i * 100);
            yield return null;
        }
    }

    IEnumerator Run()
    {
        while (true)
        {
            if (player != null && player.stats.health > 0)
            {
                player.UpdateStart();
            }

            if (player != null && player.stats.health > 0)
            {
                if (turnCount > 0)
                {
                    ResultText.lines.Add("");
                }

                ResultText.lines.Add(string.Format("=============== Turn {0} ===============", ++turnCount));

                StartPlayerTurn();

                //ingredientMenu.SetActive(true);

                for (int i = 0; i < 10; i++)
                    yield return null;

                targetIndicator.transform.position = targeted.transform.position;
                targetIndicator.gameObject.SetActive(true);

                for (int i = 0; i < 10; i++)
                {
                    targetIndicator.color = new Color(1, 1, 1, (i + 1) / 10f);
                    yield return null;
                }

                yield return new WaitUntil(() => !playerTurn);
                player.UpdateEnd();
            }

            for (int i = 0; i < 75; i++)
                yield return null;

            if (CheckState())
                break;

            foreach (Entity enemy in enemies)
            {
                if (enemy != null && enemy.stats.health > 0)
                {
                    bool canAct = enemy.UpdateStart();

                    for (int i = 0; i < 15; i++)
                        yield return null;

                    if (enemy.stats.health > 0)
                    {
                        if (canAct)
                            attackManager.ProcessAttack(enemy, player, new Entity[] { player }, enemy.GetComponent<EnemyAction>().ingredients);

                        enemy.UpdateEnd();
                    }

                    for (int i = 0; i < 75; i++)
                        yield return null;

                    if (CheckState())
                        break;
                }
            }
        }
    }

    public int GetEnemyRemaining()
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

    bool CheckState()
    {
        if (player == null)
        {
            Entity.playerStats.health = Entity.playerStats.maxHealth;
            Entity.SavePlayerStats();
            PlayerMovementFixed.spawnPosition = PlayerMovementFixed.checkpointPosition;
            EnemyDespawner.despawned.Clear();
            UnityEngine.SceneManagement.SceneManager.LoadScene(overworldScene);
            return true;
        }

        if (GetEnemyRemaining() == 0)
        {
            Entity.SavePlayerStats();
            UnityEngine.SceneManagement.SceneManager.LoadScene(overworldScene);
            return true;
        }

        return false;
    }

    public Entity GetPlayer()
    {
        return player;
    }

    public Entity[] GetEnemies()
    {
        return enemies;
    }

    public void AddEnemyToQueue(Entity enemy)
    {
        queue.Add(enemy);
    }
}
