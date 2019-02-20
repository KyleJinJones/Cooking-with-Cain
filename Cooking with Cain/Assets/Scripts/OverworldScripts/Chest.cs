using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public UpgradeInfo reward;
    public Gold playergold;
    private bool open = false;
    // Provides the Player with an item upon them opening it.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!open&&collision.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            if (reward.attributetype == "f")
            {
                GetComponent<AddIng>().adding((int)reward.upgradeamt);

            }
            else if (reward.attributetype == "i")
            { 
                PlayerPrefs.SetInt(reward.attributename, PlayerPrefs.GetInt(reward.attributename) + (int)reward.upgradeamt);
            }
            else if (reward.attributetype=="g")
            {
                playergold.UpdateGold((int)reward.upgradeamt);
            }
            open = true;
        }
    }
}
