using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveButton : MonoBehaviour
{
    public PlayerMovementFixed player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementFixed>();
    }

    public void SavePositionAndScene()
    {
        SaveDataManager.currentData.currentPosition = player.transform.position;
        SaveDataManager.currentData.sceneName = SceneManager.GetActiveScene().name;
    }
}
