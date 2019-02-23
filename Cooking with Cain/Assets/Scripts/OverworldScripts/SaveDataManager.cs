using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class SaveDataManager : MonoBehaviour
{
    public bool loadSaveData = false;
    public SaveData data;

    static SaveDataManager instance = null;

    public string dataPath;

    public static SaveData currentData
    {
        get
        {
            return instance.data;
        }
    }

    void Awake()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "save.json");

        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            if (loadSaveData)
            {
                LoadDataFromFile();
            }
        }
        else
        {
            data = instance.data;
            Destroy(instance.gameObject);
        }

        instance = this;
    }

    void LoadDataFromFile()
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

[CustomEditor(typeof(SaveDataManager))]
public class SaveDataManagerEditor : Editor
{
    SerializedProperty loadSaveData;
    SerializedProperty data;
    string dataPath;

    void OnEnable()
    {
        loadSaveData = serializedObject.FindProperty("loadSaveData");
        data = serializedObject.FindProperty("data");

        dataPath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(loadSaveData);

        if (!(target as SaveDataManager).loadSaveData)
        {
            EditorGUILayout.PropertyField(data, true);
        }

        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Delete Save File"))
        {
            File.Delete(dataPath);
        }
    }
}

[System.Serializable]
public class SaveData
{
    public Stats playerStats;
    public List<int> despawnedEnemies;
    public Vector2 currentPosition;
    public Vector2 checkpointPosition;
    public int gold;
    public Ingredient[] equipped = new Ingredient[6];
    public Ingredient[] spare = new Ingredient[12];
    public int[] potions = new int[3];
    public string sceneName;
    public List<ChestId> openedChests = new List<ChestId>();
    public List<UpgradeInfo> shopBought = new List<UpgradeInfo>();
}