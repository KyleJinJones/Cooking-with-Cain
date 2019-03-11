using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Enemy", menuName = "EnemyTheo")]
public class EnemyTheo : ScriptableObject
{
    public Sprite enemyArt;
    public Sprite enemyHealthBar;

    public int healthPoints;
    public int damage;

    public string ingredients;


}

