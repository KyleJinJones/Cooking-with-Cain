using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public static Stats playerStats = null;
    public Stats stats;

    void Start()
    {
        if (gameObject.tag == "Player")
        {
            stats = playerStats;
        }
    }
}

[System.Serializable]
public class Stats
{
    public float attack = 10;
    public float maxHealth = 100;

    public float burn = 0.1f;
    public float splash = 0.5f;
    public float lifesteal = 0.5f;
    public float atkboost = 0.2f;
    public float atkdebuff = 0.2f;
    public float stun = 0.6f;
}