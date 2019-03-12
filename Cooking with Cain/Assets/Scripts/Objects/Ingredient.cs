using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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