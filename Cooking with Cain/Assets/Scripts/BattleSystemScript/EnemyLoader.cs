using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EntityManager))]
public class EnemyLoader : MonoBehaviour
{
    // Sets the emeies that will spawn in
    //public static List<Enemy> enemies = new List<Enemy>();

    public GameObject enemyBaseObject;

    void Start()
    {
        EntityManager manager = GetComponent<EntityManager>();

        // Clones the enemy base and sets its data based on the enemy data present in the static list

        //foreach (Enemy enemy in enemies)
        //{
        //    GameObject clone = Instantiate(enemyBaseObject, enemyBaseObject.transform.parent);
        //    Entity entity = clone.GetComponent<Entity>();
        //    Image image = clone.GetComponent<Image>();
        //    EnemyAction enemyAction = clone.GetComponent<EnemyAction>();
        //    
        //    manager.AddEnemyToQueue(entity);
        //}
    }
}
