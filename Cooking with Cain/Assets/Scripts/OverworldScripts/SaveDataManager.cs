using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public SaveData data;

    public static SaveDataManager instance = null;

    string dataPath;

    public static SaveData currentData
    {
        get
        {
            return instance.data;
        }

        set
        {
            instance.data = value;
        }
    }

    void Awake()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "save.json");

        DontDestroyOnLoad(gameObject);

        if (instance != null)
        {
            data = instance.data;
            Destroy(instance.gameObject);
        }

        instance = this;
    }

    //Called to reset the currentdata to its defaults
    public void ResetData()
    {
        instance.data = new SaveData();
    }

    public void LoadDataFromFile()
    {
        data = Load(dataPath);
    }

    public void SaveDataToFile()
    {
        Save(data, dataPath);
    }

    static void Save(SaveData data, string path)
    {
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonString);
        }
    }

    static SaveData Load(string path)
    {
        using (StreamReader streamReader = File.OpenText(path))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<SaveData>(jsonString);
        }
    }
}

[System.Serializable]
public class SaveData
{
    public Stats playerStats = new Stats();
    public List<int> despawnedEnemies= new List<int>();
    public Vector2 currentPosition;
    public Vector2 checkpointPosition;
    public int gold;
    public Ingredient[] equipped = new Ingredient[6];
    public Ingredient[] spare = new Ingredient[12];
    public int[] potions = new int[3];
    public string sceneName;
    public List<ChestId> openedChests = new List<ChestId>();
    public List<LeverId> activatedLevers = new List<LeverId>();
    public List<UpgradeInfo> shopBoughtIngredient = new List<UpgradeInfo>();
    public List<UpgradeBoughtAmount> shopBoughtStats = new List<UpgradeBoughtAmount>();
    public List<int> puzzles = new List<int>();
}