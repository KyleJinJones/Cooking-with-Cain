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
            if (SaveDataManager.currentData.level < levelset)
            {
                SaveDataManager.currentData.level=levelset;
            }
        }
    }
}
