using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Entity : MonoBehaviour
{
    public static Stats playerStats = new Stats();

    public EntityManager manager;

    public string entityName = "";
    public Stats stats;

    public int goldValue;

    public bool stunnedLastTurn;

    public List<StatusInstance> statuses = new List<StatusInstance>();

    void Start()
    {
        if (gameObject.tag == "Player")
        {
            LoadPlayerStats();
            stats = playerStats;
        }
    }

    void Update()
    {
        foreach (StatusInstance status in statuses)
        {
            if (status.blink > 0)
            {
                status.blink--;
            }

            if (status.duration > 0)
            {
                if (status.fade > 0)
                {
                    status.fade--;
                }
            }
            else
            {
                status.fade--;
            }
        }

        statuses.RemoveAll(status => status.duration <= 0 && status.fade < -15);
    }

    // Saves player stats to PlayerPrefs. CT
    public static void SavePlayerStats()
    {
        PlayerPrefs.SetFloat("MaxHP", playerStats.maxHealth);
        PlayerPrefs.SetFloat("CurrentHP", playerStats.health);
        PlayerPrefs.SetFloat("LifeSteal", playerStats.lifesteal);
        PlayerPrefs.SetFloat("AtkBoost", playerStats.atkboost);
        PlayerPrefs.SetFloat("AtkDebuff", playerStats.atkdebuff);
        PlayerPrefs.SetFloat("Attack", playerStats.attack);
        PlayerPrefs.SetFloat("Burn", playerStats.burn);
        PlayerPrefs.SetFloat("Splash", playerStats.splash);
        PlayerPrefs.SetFloat("Stun", playerStats.stun);
        PlayerPrefs.SetFloat("DefBoost", playerStats.defboost);
        PlayerPrefs.SetFloat("DefDebuff", playerStats.defdebuff);
        PlayerPrefs.SetFloat("Reflect", playerStats.reflect);
        PlayerPrefs.SetFloat("Selfdmg", playerStats.selfdmg);
        PlayerPrefs.SetFloat("Miss", playerStats.miss);
    }

    //Loads each individual player stat KJ
    private void LoadPlayerStats()
    {
        playerStats.maxHealth = getStat("MaxHP", playerStats.maxHealth);
        playerStats.health = getStat("CurrentHP", playerStats.health);
        playerStats.lifesteal = getStat("LifeSteal", playerStats.lifesteal);
        playerStats.atkboost = getStat("AtkBoost", playerStats.atkboost);
        playerStats.atkdebuff = getStat("AtkDebuff", playerStats.atkdebuff);
        playerStats.attack = getStat("Attack", playerStats.attack);
        playerStats.burn = getStat("Burn", playerStats.burn);
        playerStats.splash = getStat("Splash", playerStats.splash);
        playerStats.stun = getStat("Stun", playerStats.stun);
        playerStats.defboost = getStat("DefBoost", playerStats.defboost);
        playerStats.defdebuff = getStat("DefDebuff", playerStats.defdebuff);
        playerStats.reflect = getStat("Reflect", playerStats.reflect);
        playerStats.selfdmg = getStat("Selfdmg", playerStats.selfdmg);
        playerStats.miss = getStat("Miss", playerStats.miss);
    }

    //Tries to get a Playerpref value for the stat, otherwise sets it to the base value and returns the base value KJ
    private float getStat(string stat, float baseStat)
    {
        if (PlayerPrefs.HasKey(stat))
        {
            return PlayerPrefs.GetFloat(stat);
        }
        else
        {
            PlayerPrefs.SetFloat(stat,baseStat);
            return baseStat;
        }
    }

    public StatusInstance AddStatus(StatusInstance.Status status, float potency, int duration)
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

                return instance;
            }
            else
            {
                exist.potency = potency;
                exist.duration = duration;
                exist.fade = 0;

                return exist;
            }
        }

        return null;
    }

    public virtual void UpdateStartPlayerTurn()
    {
    }

    public virtual bool UpdateStart()
    {
        StatusBlink(StatusInstance.Status.burn, StatusInstance.Status.stun);

        StatusInstance burn = statuses.Find(status => status.status == StatusInstance.Status.burn);

        if (burn != null)
        {
            int damage = Mathf.Max(Mathf.RoundToInt(stats.health * burn.potency), 1);
            ModifyHealth(-damage);

            ResultText.lines.Add(string.Format("{0} takes {1} damage from burn", entityName, damage));
        }

        stunnedLastTurn = statuses.Find(status => status.status == StatusInstance.Status.stun) != null;

        if (stunnedLastTurn)
        {
            ResultText.lines.Add(string.Format("{0} cannot act", entityName));
        }

        foreach (StatusInstance status in statuses.FindAll(instance => instance.updateOnStart()))
        {
            status.duration--;
        }

        return !stunnedLastTurn;
    }

    public virtual void UpdateEnd()
    {
        foreach (StatusInstance status in statuses.FindAll(instance => !instance.updateOnStart()))
        {
            status.duration--;
        }
    }

    public float GetEffectiveAttack()
    {
        float attack = stats.attack;

        StatusInstance atkboost = statuses.Find(status => status.status == StatusInstance.Status.atkup);
        StatusInstance atkdebuff = statuses.Find(status => status.status == StatusInstance.Status.atkdown);
        
        return attack + (atkboost == null ? 0 : attack * atkboost.potency) - (atkdebuff == null ? 0 : attack * atkdebuff.potency);
    }

    public virtual float FactorDefense(float damage)
    {
        StatusInstance defboost = statuses.Find(status => status.status == StatusInstance.Status.defup);
        StatusInstance defdebuff = statuses.Find(status => status.status == StatusInstance.Status.defdown);

        return damage - (defboost == null ? 0 : damage * defboost.potency) + (defdebuff == null ? 0 : damage * defdebuff.potency);
    }

    public virtual void ModifyHealth(int health)
    {
        stats.health += health;

        if (stats.health > stats.maxHealth)
        {
            stats.health = stats.maxHealth;
        }
        else if (Mathf.RoundToInt(stats.health) <= 0)
        {
            stats.health = 0;
            statuses.Clear();
            StartCoroutine(Die());
        }
        else if (health < 0)
        {
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        for (int i = 0; i < 5; i++)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1 - i / 5f);
            yield return null;
        }

        for (int i = 1; i <= 5; i++)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, i / 5f);
            yield return null;
        }
    }

    IEnumerator Die()
    {
        ResultText.lines.Add(string.Format("{0} is defeated", entityName));

        if (goldValue > 0)
        {
            PlayerPrefs.SetInt("gold", (PlayerPrefs.HasKey("gold") ? PlayerPrefs.GetInt("gold") : 0) + goldValue);
            ResultText.lines.Add(string.Format("{0} gold gained", goldValue));
        }

        if (gameObject.tag == "Player")
        {
            int lost = Mathf.RoundToInt(PlayerPrefs.GetInt("gold") * 0.2f);
            PlayerPrefs.SetInt("gold", (PlayerPrefs.HasKey("gold") ? PlayerPrefs.GetInt("gold") : 0) - lost);
            ResultText.lines.Add(string.Format("{0} gold lost", lost));

            for (int i = 60; i > 0; i--)
            {
                GetComponent<Image>().color = new Color(1, 1, 1, i / 60f);
                transform.Rotate(0, 0, 15);
                transform.localPosition = transform.localPosition + new Vector3(-5, i - 40);
                yield return null;
            }
        }
        else
        {
            for (int i = 30; i > 0; i--)
            {
                GetComponent<Image>().color = new Color(1, 1, 1, i / 30f);
                transform.Rotate(0, 0, 15);
                transform.localPosition = transform.localPosition + new Vector3(30, 10);
                yield return null;
            }
        }

        Destroy(gameObject);
    }

    public void StatusBlink(params StatusInstance.Status[] types)
    {
        List<StatusInstance.Status> statusTypes = new List<StatusInstance.Status>();
        statusTypes.AddRange(types);

        foreach (StatusInstance status in statuses.FindAll(instance => statusTypes.Contains(instance.status)))
        {
            status.blink = 15;
        }
    }
}

[System.Serializable]
public class StatusInstance
{
    public enum Status { burn, atkup, atkdown, stun, defup, defdown, reflect, cleanse };

    public Status status;
    public float potency;
    public int duration;
    
    public int blink = 0;
    public int fade = 15;

    public string customMessage = null;

    public bool updateOnStart()
    {
        return status == Status.defup || status == Status.defdown || status == Status.reflect || status == Status.cleanse;
    }
}

[System.Serializable]
public class Stats
{
    public float attack = 10;
    public float health = 100;
    public float maxHealth = 100;

    public float burn = 0.07f;
    public float splash = 0.5f;
    public float lifesteal = 0.5f;
    public float atkboost = 0.2f;
    public float atkdebuff = 0.2f;
    public float stun = 0.6f;
    public float defboost = 0.2f;
    public float defdebuff = 0.4f;
    public float reflect = 0.3f;
    public float selfdmg = 0.1f;
    public float miss = 0.4f;

    public Stats copy
    {
        get
        {
            Stats stats = new Stats();
            stats.attack = attack;
            stats.health = health;
            stats.maxHealth = maxHealth;
            stats.burn = burn;
            stats.splash = splash;
            stats.lifesteal = lifesteal;
            stats.atkboost = atkboost;
            stats.atkdebuff = atkdebuff;
            stats.stun = stun;
            stats.defboost = defboost;
            stats.defdebuff = defdebuff;
            stats.reflect = reflect;
            stats.selfdmg = selfdmg;
            stats.miss = miss;
            return stats;
        }
    }
}