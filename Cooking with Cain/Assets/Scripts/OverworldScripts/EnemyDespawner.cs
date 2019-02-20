using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EnemyDespawner : MonoBehaviour
{
    // List of enemies that are defeated and won't spawn. Needs to be cleared when transitioning between areas. CT
    public static List<int> despawned = new List<int>();
    
    // ID of the enemy. CT
    public int enemyID;
    
    public void AutoAssignAllEnemyId()
    {
        int nextId = 0;

        foreach (EnemyDespawner despawner in FindObjectsOfType<EnemyDespawner>())
        {
            Undo.RecordObject(despawner, "Auto Assign ID");
            despawner.enemyID = nextId;
            nextId++;
        }
    }

    void Start()
    {
        if (despawned.Contains(enemyID))
        {
            Destroy(gameObject);
        }
    }
}

[CustomEditor(typeof(EnemyDespawner))]
public class EnemyDespawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Auto Assign All Enemy ID"))
        {
            ((EnemyDespawner)target).AutoAssignAllEnemyId();
        }
    }
}