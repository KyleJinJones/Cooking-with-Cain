using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public static Stats playerStats = null;
    public Stats stats;
    public List<StatusInstance> statuses = new List<StatusInstance>();

    void Start()
    {
        if (gameObject.tag == "Player")
        {
            if (playerStats == null)
                playerStats = new Stats();

            stats = playerStats;
        }
    }

    public bool AddStatus(StatusInstance.Status status, float potency, int duration)
    {
        if (statuses.Find(instance => instance.status == status) == null)
        {
            StatusInstance instance = new StatusInstance();
            instance.status = status;
            instance.potency = potency;
            instance.duration = duration;
            statuses.Add(instance);

            return true;
        }

        return false;
    }

    public void UpdateStart()
    {
        ;
    }

    public void UpdateEnd()
    {
        ;
    }
}

[System.Serializable]
public class StatusInstance
{
    public enum Status { burn, atkup, atkdown, stun };

    public Status status;
    public float potency;
    public int duration;
}

[System.Serializable]
public class Stats
{
    public float attack = 10;
    public float health = 100;
    public float maxHealth = 100;

    public float burn = 0.1f;
    public float splash = 0.5f;
    public float lifesteal = 0.5f;
    public float atkboost = 0.2f;
    public float atkdebuff = 0.2f;
    public float stun = 0.6f;
}