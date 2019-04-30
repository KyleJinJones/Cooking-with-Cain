using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public GameObject overworldHP;

    private void Start()
    {
        overworldHP = FindObjectOfType<OverworldHP>().gameObject;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovementFixed.spawnPosition = collision.transform.position;
            overworldHP.GetComponent<OverworldHP>().currenthp = 100;
            overworldHP.GetComponent<OverworldHP>().hptext.text = "100";
            overworldHP.transform.GetChild(2).GetComponent<Image>().fillAmount = 1;
            SaveDataManager.currentData.playerStats.health = 100;
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("asdf");
        PlayerMovementFixed.checkpointPosition = collision.transform.position;
    }

}
