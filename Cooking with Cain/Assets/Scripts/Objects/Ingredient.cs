using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Ingredient.asset", menuName = "Cooking with Cain/Ingredient")]
public class Ingredient : ScriptableObject
{
    public enum Attribute { none, burn, splash, atkup, leech, atkdown, stun, defup, defdown, selfdmg, miss, cleanse, reflect };
    public enum DamageType { flat, range };

    public string foodName;
    public Sprite sprite;
    public AudioClip audioClip;

    public DamageType damageType;
    public float multiplier;
    public float multiplierMin;
    public float multiplierMax;

    public Attribute attribute;
}

[CustomEditor(typeof(Ingredient))]
public class IngredientEditor: Editor
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

        switch((target as Ingredient).damageType)
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