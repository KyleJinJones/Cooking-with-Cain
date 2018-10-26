using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deal_Damage : MonoBehaviour {
    public GameObject manager;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void processFood(GameObject attacker, GameObject target, int attack, GameObject[] ingredients)
    {
        float spamMod = 0.8f;

        AttributeStats stats = attacker.GetComponent<AttributeStats>();

        float totalDamage = 0;
        List<string> attributes = new List<string>();

        foreach(GameObject foodObject in ingredients)
        {
            Food food = foodObject.GetComponent<Food>();

            totalDamage += attack * (food.multiplierRange.Length == 2 ? Random.Range(food.multiplierRange[0], food.multiplierRange[1]) : food.multiplier);


            attributes.Add(foodObject.GetComponent<Food>().attribute);

            if (!attacker.GetComponent<Attack>().lastPlayed.Contains(foodObject.GetComponent<Food>().attribute) || attacker.tag.Equals("Enemy"))
            {
                spamMod = 1;
            }
        }

        if (attributes.Contains("lifesteal"))
        {
            attacker.GetComponent<Health>().heal(Mathf.RoundToInt(totalDamage * stats.lifesteal));
        }

        if (attributes.Contains("splash"))
        {
            if (target.tag.Equals("Enemy"))
                foreach(GameObject targets in manager.GetComponent<Turn_Manager>().getEnemies())
                   damage(attacker, targets, Mathf.RoundToInt(totalDamage * stats.splash * spamMod), attributes.Contains("burn"), stats.burn);
            else foreach (GameObject targets in manager.GetComponent<Turn_Manager>().getPlayers())
                damage(attacker, targets, Mathf.RoundToInt(totalDamage * stats.splash * spamMod), attributes.Contains("burn"), stats.burn);
        }
        else
        {
            damage(attacker, target, Mathf.RoundToInt(totalDamage * spamMod), attributes.Contains("burn"), stats.burn);
        }

        if (attributes.Contains("atkboost"))
        {
            attacker.GetComponent<Attack>().boostDuration = 2;
        }
    }

	public void damage(GameObject Attacker,GameObject target, int amt, bool burn, float burnPercent) {
        Health health = target.GetComponent<Health>();

        health.damage(amt);
        
        if (burn)
        {
            health.burnDuration = 3;

            if (health.burnPercent < burnPercent)
                health.burnPercent = burnPercent;
        }
    }
}
