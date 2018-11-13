using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deal_Damage : MonoBehaviour {
    public GameObject manager;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void processFood(GameObject attacker, GameObject target, float attack, List<Food> ingredients)
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
            {
                foreach (GameObject targets in manager.GetComponent<Turn_Manager>().getEnemies())
                    if (targets != null)
                        damage(attacker, targets, Mathf.RoundToInt(totalDamage * stats.splash), attributes.Contains("burn"), attributes.Contains("atkdebuff"), attributes.Contains("stun"));
            } 
            else foreach (GameObject targets in manager.GetComponent<Turn_Manager>().getPlayers())
                    if (targets != null)
                        damage(attacker, targets, Mathf.RoundToInt(totalDamage * stats.splash), attributes.Contains("burn"), attributes.Contains("atkdebuff"),attributes.Contains("stun"));
        }
        else
        {
            damage(attacker, target, Mathf.RoundToInt(totalDamage), attributes.Contains("burn"), attributes.Contains("atkdebuff"),attributes.Contains("stun"));
        }

        if (attributes.Contains("atkboost"))
        {
            attacker.GetComponent<Attack>().boostDuration = 2;
        }
    }

	public void damage(GameObject attacker,GameObject target, int amt, bool burn, bool atkDebuff, bool stun) {
        Health health = target.GetComponent<Health>();

        health.damage(amt);
        
        if (burn)
        {
			float burnPercent = attacker.GetComponent<AttributeStats>().burn;
            health.burnDuration = 3;

            if (health.burnPercent < burnPercent)
                health.burnPercent = burnPercent;
        }
		
		if (atkDebuff)
        {
            Debug.Log("debuffed");
			Attack attack = target.GetComponent<Attack>();
			float debuffPercent = attacker.GetComponent<AttributeStats>().atkdebuff;
            attack.debuffDuration = 3;

            if (attack.debuffPercent < debuffPercent)
            {
                attack.debuffPercent = debuffPercent;
            }
        }
        //implements the stun mechanic, note does not work on players
        if(stun)
        {
            float stunchance = attacker.GetComponent<AttributeStats>().stun;
            float chance = Random.Range(1, 101);
            //If the enemies were just stunned, halves their chance of being stunned again
            if (target.GetComponent<Enemy_Turn>().juststunned)
            {
                stunchance *=.5f;
            }
            if (stunchance > chance)
            {
               
                target.GetComponent<Enemy_Turn>().stunned = true;
            }

        }
    }
}
