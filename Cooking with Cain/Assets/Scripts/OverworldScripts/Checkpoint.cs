using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public OverworldHP ohp;

    private void Start()
    {
        ohp = FindObjectOfType<OverworldHP>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovementFixed.spawnPosition = collision.transform.position;
            ohp.ChangeHP(SaveDataManager.currentData.playerStats.maxHealth - SaveDataManager.currentData.playerStats.health);
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerMovementFixed.checkpointPosition = collision.transform.position;
    }

}
