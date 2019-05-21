using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EntityManager))]
public class EnemyLoader : MonoBehaviour
{
    // Sets the emeies that will spawn in
    public static List<Enemy> enemies = new List<Enemy>();
    public static List<GameObject> bosses = new List<GameObject>();

    public GameObject enemyBaseObject;

    // Spawns in enemies when loading the battle scene directly for testing purposes. CT
    public List<Enemy> testEnemies = new List<Enemy>();
    public List<GameObject> testBosses = new List<GameObject>();

    EntityManager manager;

    void Awake()
    {
        manager = GetComponent<EntityManager>();

        foreach (GameObject prefab in bosses)
        {
            manager.AddEnemyToQueue(GenerateBoss(prefab));
        }

        foreach (GameObject prefab in testBosses)
        {
            manager.AddEnemyToQueue(GenerateBoss(prefab));
        }

        // Clones the enemy base and sets its data based on the enemy data present in the static list


        foreach (Enemy enemy in enemies)
        {
            manager.AddEnemyToQueue(GenerateEnemy(enemy));
        }

        foreach (Enemy enemy in testEnemies)
        {
            manager.AddEnemyToQueue(GenerateEnemy(enemy));
        }
    }

    public Entity GenerateBoss(GameObject prefab)
    {
        GameObject clone = Instantiate(prefab, enemyBaseObject.transform.parent);
        Entity entity = clone.GetComponent<Entity>();
        entity.manager = manager;
        entity.loader = this;

        return entity;
    }

    public Entity GenerateEnemy(Enemy enemy)
    {
        GameObject clone = Instantiate(enemyBaseObject, enemyBaseObject.transform.parent);
        Entity entity = clone.GetComponent<Entity>();
        Image image = clone.GetComponent<Image>();
        EnemyAction enemyAction = clone.GetComponent<EnemyAction>();

        entity.entityName = enemy.enemyName;
        entity.stats = enemy.stats.copy;
        image.sprite = enemy.sprite;
        enemyAction.ingredients = enemy.ingredients;
        entity.goldValue = enemy.goldValue;
        entity.ingreward = enemy.ingreward;
        entity.attackSound = enemy.attackSound[Random.Range(0, enemy.deathSound.Length)];
        entity.deathSound = enemy.deathSound[Random.Range(0,enemy.deathSound.Length)];

        TooltipTextWithIngredients tooltip = clone.AddComponent<TooltipTextWithIngredients>();
        tooltip.text = entity.entityName;
        //tooltip.text = "Ingredients:";
        List<Ingredient> ingredients = new List<Ingredient>();
        ingredients.AddRange(enemy.ingredients);
        //tooltip.sprites = ingredients.ConvertAll(ingredient => ingredient.sprite);

        entity.manager = manager;
        entity.loader = this;

        return entity;
    }
}
