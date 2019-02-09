using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    // The enemies that spawn in the battle scene
    // Waiting for the enemy scritable object to be created
    public List<Enemy> enemies = new List<Enemy>();

    // In case we have battles that don't start from colliding with an enemy
    public bool encounterOnCollision;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        // [need change the tag to the tag used for the player]
        if (encounterOnCollision && collision.tag == "")
        {
            // collision.transform.position;
            StartEncounter();
        }
    }

    public void StartEncounter()
    {
        // Sets the enemy loader enemy list to this script's enemy list
        EnemyLoader.enemies = enemies;

        // Loads the battle scene
        SceneManager.LoadScene("Battle");
    }
}
