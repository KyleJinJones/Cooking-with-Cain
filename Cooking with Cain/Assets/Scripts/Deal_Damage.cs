using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deal_Damage : MonoBehaviour {
    public GameObject manager;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void processFood(GameObject attacker, GameObject target, int attack, List<Food> ingredients)
    {
        AttributeStats stats = attacker.GetComponent<AttributeStats>();

        float totalDamage = 0;
        List<string> attributes = new List<string>();
        List<string> played = new List<string>();

        foreach(Food food in ingredients)
        {

            totalDamage += attack * (food.multiplierRange.Length == 2 ? Random.Range(food.multiplierRange[0], food.multiplierRange[1]) : food.multiplier);


            attributes.Add(food.GetComponent<Food>().attribute);
            played.Add(food.foodName);
        }

        if (attacker.tag.Equals("Player"))
        {
            if (attacker.GetComponent<Attack>().lastPlayed.Capacity > 0 && !attacker.GetComponent<Attack>().lastPlayed.Exists(f => !played.Contains(f)))
                totalDamage *= 0.8f;

            attacker.GetComponent<Attack>().lastPlayed = played;
        }

        if (attributes.Contains("lifesteal"))
        {
            attacker.GetComponent<Health>().heal(Mathf.RoundToInt(totalDamage * stats.lifesteal));
        }

        if (attributes.Contains("splash"))
        {
            if (target.tag.Equals("Enemy"))
                foreach(GameObject targets in manager.GetComponent<Turn_Manager>().getEnemies())
                   damage(attacker, targets, Mathf.RoundToInt(totalDamage * stats.splash), attributes.Contains("burn"), stats.burn);
            else foreach (GameObject targets in manager.GetComponent<Turn_Manager>().getPlayers())
                damage(attacker, targets, Mathf.RoundToInt(totalDamage * stats.splash), attributes.Contains("burn"), stats.burn);
        }
        else
        {
            damage(attacker, target, Mathf.RoundToInt(totalDamage), attributes.Contains("burn"), stats.burn);
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
