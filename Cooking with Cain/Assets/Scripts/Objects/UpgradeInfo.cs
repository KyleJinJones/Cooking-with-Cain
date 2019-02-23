using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "UpgradeInfo.asset", menuName = "Cooking with Cain/UpgradeInfo")]
public class UpgradeInfo : ScriptableObject
{
    public enum AttributeType { STAT, INGREDIENT, GOLD, POTION }

    public AttributeType attributeType;
    public int goldcost;
    public Sprite upgradeimage;
    public string infotext;

    public Stats statsModification = Stats.zero;

    public Ingredient ingredient;

    public HPpotion.PotionType potionType;
    public int amount;

    public void obtain()
    {
        switch (attributeType)
        {
            case AttributeType.STAT:
                Entity.playerStats.Add(statsModification);
                break;
            case AttributeType.INGREDIENT:
                for (int i = 0; i < 12; i++)
                {
                    if (IngredientManager.spareIngredients[i] == null)
                        IngredientManager.spareIngredients[i] = ingredient;
                }
                break;
            case AttributeType.GOLD:
                Gold.gold += amount;
                break;
            case AttributeType.POTION:
                HPpotion.potions[(int)potionType] += amount;
                break;
        }
    }
}

[CustomEditor(typeof(UpgradeInfo))]
public class UpgradeInfoEditor : Editor
{
    SerializedProperty attributeType;
    SerializedProperty goldcost;
    SerializedProperty upgradeimage;
    SerializedProperty infotext;

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

        switch (attributeType.enumValueIndex)
        {
            case (int) UpgradeInfo.AttributeType.STAT:
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