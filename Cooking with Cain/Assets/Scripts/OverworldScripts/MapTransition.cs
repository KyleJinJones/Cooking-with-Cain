﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTransition : MonoBehaviour
{
    // Sets the scene to transition to. CT
    public string sceneName;
    // Sets spawn position in the new scene. CT
    public Vector2 spawnPosition;

    public GameObject interactPopup;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactPopup.SetActive(true);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.tag == "Player")
        {
            TransitionScene();

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactPopup.SetActive(false);
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
