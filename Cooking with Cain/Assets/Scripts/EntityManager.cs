using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AttackManager))]
public class EntityManager : MonoBehaviour
{
    Entity player;
    public Entity[] enemies = new Entity[3];
    bool playerTurn = false;

    AttackManager attackManager;

    public Image targetIndicator;

    public List<Entity> queue = new List<Entity>();

    public Entity targeted = null;

    public GameObject ingredientMenu;

    public Vector3[] positions = new Vector3[3];

    public Text enemyRemaining;

    void Awake()
    {
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
            ingredientMenu.SetActive(false);
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
                StartPlayerTurn();

                if (player.statuses.Find(status => status.status == StatusInstance.Status.burn) != null)
                {
                    for (int i = 0; i < 45; i++)
                        yield return null;

                    ResultText.FadeReset();

                    for (int i = 0; i < 30; i++)
                        yield return null;
                }

                ingredientMenu.SetActive(true);

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

            for (int i = 0; i < 45; i++)
                yield return null;

            ResultText.FadeReset();

            for (int i = 0; i < 30; i++)
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

                    for (int i = 0; i < 45; i++)
                        yield return null;

                    ResultText.FadeReset();

                    for (int i = 0; i < 30; i++)
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
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            return true;
        }

        if (GetEnemyRemaining() == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
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
