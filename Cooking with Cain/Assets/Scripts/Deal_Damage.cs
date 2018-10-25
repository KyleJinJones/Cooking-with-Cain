using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deal_Damage : MonoBehaviour {
    public void processFood(GameObject attacker, GameObject target, int attack, GameObject[] ingredients)
    {
        float totalDamage = 0;
        List<string> attributes = new List<string>();

        foreach(GameObject foodObject in ingredients)
        {
            Food food = foodObject.GetComponent<Food>();

            totalDamage += attack * food.multiplier;
            
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
            // obtain list of targets via Turn_Manager?
            damage(attacker, target, Mathf.RoundToInt(totalDamage), attributes.Contains("burn"));
        }
        else
        {
            damage(attacker, target, Mathf.RoundToInt(totalDamage), attributes.Contains("burn"));
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
