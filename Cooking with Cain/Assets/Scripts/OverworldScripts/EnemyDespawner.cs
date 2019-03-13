﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDespawner : MonoBehaviour
{
    // List of enemies that are defeated and won't spawn. Needs to be cleared when transitioning between areas. CT
    public static List<int> despawned
    {
        get
        {
            return SaveDataManager.currentData.despawnedEnemies;
        }
    }
    
    // ID of the enemy. CT
    public int enemyID;

    void Start()
    {
        if (despawned.Contains(enemyID))
        {
            Destroy(gameObject);
        }
    }
}
