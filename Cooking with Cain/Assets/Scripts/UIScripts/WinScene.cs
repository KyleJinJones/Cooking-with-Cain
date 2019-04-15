using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    public static string lastScene;

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ContinuePlaying()
    {
        PlayerMovementFixed.spawnPosition = PlayerMovementFixed.checkpointPosition;
        SceneManager.LoadScene(lastScene);
    }

    public void Credits()
    {
        SceneManager.LoadScene(2);
    }
}
