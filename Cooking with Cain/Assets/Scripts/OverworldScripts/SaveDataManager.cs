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
    public static SaveData currentData
    {
        get
        {
            return instance.data;
        }
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (loadSaveData)
        {
            LoadDataFromFile();
        }
    }

    void LoadDataFromFile()
    {
        ;
    }

    public void SaveDataToFile()
    {
        ;
    }
}

[CustomEditor(typeof(SaveDataManager))]
public class SaveDataManagerEditor : Editor
{
    SerializedProperty loadSaveData;
    SerializedProperty data;

    void OnEnable()
    {
        loadSaveData = serializedObject.FindProperty("loadSaveData");
        data = serializedObject.FindProperty("data");
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
}