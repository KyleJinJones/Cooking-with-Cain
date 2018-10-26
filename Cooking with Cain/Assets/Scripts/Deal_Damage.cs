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
        float totalDamage = 0;
        List<string> attributes = new List<string>();

        foreach(GameObject foodObject in ingredients)
        {
            Food food = foodObject.GetComponent<Food>();

            totalDamage += attack * (food.multiplierRange.Length == 2 ? Random.Range(food.multiplierRange[0], food.multiplierRange[1]) : food.multiplier);


            foreach (string attribute in food.attributes)
            {
                attributes.Add(attribute);
            }
        }

        if (attributes.Contains("lifesteal"))
        {
            attacker.GetComponent<Health>().heal(Mathf.RoundToInt(totalDamage * 0.15f));
        }

        if (attributes.Contains("splash"))
        {
            GameObject[] enemies = manager.GetComponent<Turn_Manager>().enemies;
            GameObject[] players = manager.GetComponent<Turn_Manager>().enemies;

            if (UnityEditor.ArrayUtility.Contains<GameObject>(enemies, target))
                foreach(GameObject targets in enemies)
                   damage(attacker, targets, Mathf.RoundToInt(totalDamage / 2), attributes.Contains("burn"));
            else foreach (GameObject targets in players)
                    damage(attacker, targets, Mathf.RoundToInt(totalDamage / 2), attributes.Contains("burn"));
        }
        else
        {
            damage(attacker, target, Mathf.RoundToInt(totalDamage), attributes.Contains("burn"));
        }

        if (attributes.Contains("atkboost"))
        {
            // gives attack boost to attacker
        }
    }

	public void damage(GameObject Attacker,GameObject target, int amt, bool burn) {
        Health health = target.GetComponent<Health>();

        health.damage(amt);
        
        if (burn)
        {
            health.burnDuration = 3;
        }
    }
}
