using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy.asset", menuName = "Cooking with Cain/Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public Sprite sprite;
    public Stats stats;
    public Ingredient[] ingredients;
    public int goldValue;
    public UpgradeInfo ingreward;

    public AudioClip[] attackSound;
    public AudioClip[] deathSound;
}
