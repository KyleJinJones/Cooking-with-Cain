using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StartMenuScript : MonoBehaviour
{
    public List<Ingredient> startinging;

    public void StartGame()
    {
        File.Delete(Path.Combine(Application.persistentDataPath, "save.json"));
        PlayerPrefs.DeleteAll();
        SaveDataManager.instance.ResetData();

        int temp = 0;
        foreach(Ingredient i in startinging)
        {
            SaveDataManager.currentData.equipped[temp] = i;
            temp++;
        }
        SaveDataManager.currentData.gold = 100;
        SaveDataManager.currentData.currentPosition =new Vector2(0, 0);
        SaveDataManager.currentData.checkpointPosition = new Vector2(0, 0);
        PlayerMovementFixed.spawnPosition = new Vector2(0, 0);
        print(SaveDataManager.currentData.checkpointPosition);
        SceneManager.LoadScene("Intro CS");
    }

    public void Continue()
    {
        PlayerMovementFixed.spawnPosition = SaveDataManager.currentData.currentPosition;
        SceneManager.LoadScene(SaveDataManager.currentData.sceneName);
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void SceneTransition(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit(){
        Application.Quit();
    }
}
