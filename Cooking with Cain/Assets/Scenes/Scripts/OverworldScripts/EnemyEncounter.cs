using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    // The enemies that spawn in the battle scene. CT
    public List<Enemy> enemies = new List<Enemy>();

    public virtual void StartEncounter(GameObject player)
    {
        // Sets the enemy loader enemy list to this script's enemy list. CT
        EnemyLoader.enemies = enemies;
        // Sets the player spawn position when the scene transitions back. CT
        PlayerMovementFixed.spawnPosition = player.transform.position;
        // Sets the scene to transition back to after the battle. CT
        EntityManager.overworldScene = SceneManager.GetActiveScene().name;
        EntityManager.overworldSceneLose = SceneManager.GetActiveScene().name;
        // Adds the enemy to the list of despawned enemies. CT
        EnemyDespawner despawner;
        if ((despawner = GetComponent<EnemyDespawner>()) != null)
            EnemyDespawner.despawned.Add(despawner.enemyID);
        // Loads the battle scene. CT
        SceneManager.LoadScene("Battle");
    }
}
