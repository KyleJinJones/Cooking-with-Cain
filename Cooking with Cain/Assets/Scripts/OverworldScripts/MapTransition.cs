using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTransition : MonoBehaviour
{
    // Sets the scene to transition to. CT
    public string sceneName;
    // Sets spawn position in the new scene. CT
    public Vector2 spawnPosition;
    
    // When the collider is entered but only if it is a trigger. CT
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TransitionScene();
        }
    }

    // Transitions to new scene, sets the new spawn and checkpoint position, and respawns all enemies. CT
    public void TransitionScene()
    {
        PlayerMovementFixed.spawnPosition = spawnPosition;
        PlayerMovementFixed.checkpointPosition = spawnPosition;
        EnemyDespawner.despawned.Clear();
        SceneManager.LoadScene(sceneName);
    }
}
