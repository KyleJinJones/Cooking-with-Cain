using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawner : MonoBehaviour
{
    // List of enemies that are defeated and won't spawn. Needs to be cleared when transitioning between areas. CT
    public static List<int> despawned = new List<int>();

    // The ID to give the next enemy that loads. CT
    public static int nextID = 0;
    
    // ID of the enemy. CT
    public int enemyID;

    void Awake()
    {
        nextID = 0;
    }

    void Start()
    {
        enemyID = nextID;
        nextID++;

        if (despawned.Contains(enemyID))
        {
            Destroy(gameObject);
        }
    }
}
