using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveButton : MonoBehaviour
{
    public PlayerMovementFixed player;

    public void SavePositionAndScene()
    {
        SaveDataManager.currentData.currentPosition = player.transform.position;
        SaveDataManager.currentData.sceneName = SceneManager.GetActiveScene().name;
    }
}
