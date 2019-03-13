using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(UpgradeInfo))]
public class UpgradeInfoEditor : Editor
{
    SerializedProperty attributeType;
    SerializedProperty goldcost;
    SerializedProperty upgradeimage;
    SerializedProperty infotext;
    SerializedProperty rewardname;

    SerializedProperty limit;
    SerializedProperty costIncrease;
    SerializedProperty statsModification;

    SerializedProperty ingredient;

    SerializedProperty potionType;
    SerializedProperty amount;

    void OnEnable()
    {
        attributeType = serializedObject.FindProperty("attributeType");
        goldcost = serializedObject.FindProperty("goldcost");
        upgradeimage = serializedObject.FindProperty("upgradeimage");
        infotext = serializedObject.FindProperty("infotext");
        rewardname = serializedObject.FindProperty("rewardname");

        limit = serializedObject.FindProperty("limit");
        costIncrease = serializedObject.FindProperty("costIncrease");
        statsModification = serializedObject.FindProperty("statsModification");

        ingredient = serializedObject.FindProperty("ingredient");

        potionType = serializedObject.FindProperty("potionType");
        amount = serializedObject.FindProperty("amount");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(attributeType);
        EditorGUILayout.PropertyField(goldcost);
        EditorGUILayout.PropertyField(upgradeimage);
        EditorGUILayout.PropertyField(infotext);
        EditorGUILayout.PropertyField(rewardname);


        switch (attributeType.enumValueIndex)
        {
            case (int)UpgradeInfo.AttributeType.STAT:
                EditorGUILayout.PropertyField(limit);
                EditorGUILayout.PropertyField(costIncrease);
                EditorGUILayout.PropertyField(statsModification, true);
                break;
            case (int)UpgradeInfo.AttributeType.INGREDIENT:
                EditorGUILayout.PropertyField(ingredient);
                break;
            case (int)UpgradeInfo.AttributeType.GOLD:
                EditorGUILayout.PropertyField(amount);
                break;
            case (int)UpgradeInfo.AttributeType.POTION:
                EditorGUILayout.PropertyField(potionType);
                EditorGUILayout.PropertyField(amount);
                break;
        }
        serializedObject.ApplyModifiedProperties();
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
            int nextId = 0;
            
            foreach (EnemyDespawner despawner in FindObjectsOfType<EnemyDespawner>())
            {
                Undo.RecordObject(despawner, "Auto assign id");
                despawner.enemyID = nextId;
                nextId++;
            }
        }
    }
}

[CustomEditor(typeof(SaveDataManager))]
public class SaveDataManagerEditor : Editor
{
    string dataPath;

    void OnEnable()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Delete Save File"))
        {
            File.Delete(dataPath);
        }
    }
}

[CustomEditor(typeof(Chest))]
public class ChestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Auto Assign All Chest ID"))
        {
            int nextId = 0;

            foreach (Chest despawner in FindObjectsOfType<Chest>())
            {
                Undo.RecordObject(despawner, "Auto assign id");
                despawner.id = nextId;
                nextId++;
            }
        }
    }
}

[CustomEditor(typeof(Ingredient))]
public class IngredientEditor : Editor
{
    SerializedProperty foodName;
    SerializedProperty sprite;
    SerializedProperty audioClip;

    SerializedProperty damageType;
    SerializedProperty multiplier;
    SerializedProperty multiplierMin;
    SerializedProperty multiplierMax;

    SerializedProperty attribute;

    void OnEnable()
    {
        foodName = serializedObject.FindProperty("foodName");
        sprite = serializedObject.FindProperty("sprite");
        audioClip = serializedObject.FindProperty("audioClip");

        damageType = serializedObject.FindProperty("damageType");
        multiplier = serializedObject.FindProperty("multiplier");
        multiplierMin = serializedObject.FindProperty("multiplierMin");
        multiplierMax = serializedObject.FindProperty("multiplierMax");

        attribute = serializedObject.FindProperty("attribute");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(foodName);
        EditorGUILayout.PropertyField(sprite);
        EditorGUILayout.PropertyField(audioClip);

        EditorGUILayout.PropertyField(damageType);

        switch ((target as Ingredient).damageType)
        {
            case Ingredient.DamageType.flat:
                EditorGUILayout.PropertyField(multiplier);
                break;
            case Ingredient.DamageType.range:
                EditorGUILayout.PropertyField(multiplierMin);
                EditorGUILayout.PropertyField(multiplierMax);
                break;
        }
        EditorGUILayout.PropertyField(attribute);
        serializedObject.ApplyModifiedProperties();
    }
}