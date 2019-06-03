using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    public int levelset = 1;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E)&&collision.CompareTag("Player"))
        {
            if (!PlayerPrefs.HasKey("Level"))
            {
                PlayerPrefs.SetInt("Level", levelset);
                return;
            }

            if (PlayerPrefs.GetInt("Level") < levelset)
            {
                PlayerPrefs.SetInt("Level", levelset);
            }
        }
    }
}
