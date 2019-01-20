using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Entity : MonoBehaviour
{
    public static Stats playerStats = null;
    public string entityName = "";
    public Stats stats;

    public bool stunnedLastTurn;

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
        if (stats.health > 0)
        {
            StatusInstance exist = statuses.Find(statusInstance => statusInstance.status == status);

            if (exist == null)
            {
                StatusInstance instance = new StatusInstance();
                instance.status = status;
                instance.potency = potency;
                instance.duration = duration;
                statuses.Add(instance);
            }
            else
            {
                exist.potency = potency;
                exist.duration = duration;
            }

            return true;
        }

        return false;
    }

    public bool UpdateStart()
    {
        StatusInstance burn = statuses.Find(status => status.status == StatusInstance.Status.burn);

        if (burn != null)
        {
            ModifyHealth(-stats.health * burn.potency);
        }

        stunnedLastTurn = statuses.Find(status => status.status == StatusInstance.Status.stun) != null;
        return !stunnedLastTurn;
    }

    public void UpdateEnd()
    {
        foreach (StatusInstance status in statuses)
        {
            status.duration--;
        }

        statuses.RemoveAll(status => status.duration <= 0);
    }

    public float GetEffectiveAttack()
    {
        float attack = stats.attack;

        StatusInstance atkboost = statuses.Find(status => status.status == StatusInstance.Status.atkup);
        StatusInstance atkdebuff = statuses.Find(status => status.status == StatusInstance.Status.atkdown);

        return attack + (atkboost == null ? 0 : attack * atkboost.potency) - (atkdebuff == null ? 0 : attack * atkdebuff.potency);
    }

    public void ModifyHealth(float health)
    {
        stats.health += health;

        if (stats.health > stats.maxHealth)
        {
            stats.health = stats.maxHealth;
        }
        else if (stats.health < 0)
        {
            stats.health = 0;
            statuses.Clear();
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        if (gameObject.tag == "Player")
        {
            for (int i = 60; i > 0; i--)
            {
                GetComponent<Image>().color = new Color(1, 1, 1, i / 60f);
                transform.Rotate(0, 0, 15);
                transform.localPosition = transform.position + new Vector3(-5, i - 40);
                yield return null;
            }
        }
        else
        {
            for (int i = 30; i > 0; i--)
            {
                GetComponent<Image>().color = new Color(1, 1, 1, i / 30f);
                transform.Rotate(0, 0, 15);
                transform.localPosition = transform.position + new Vector3(30, 10);
                yield return null;
            }
        }

        Destroy(gameObject);
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