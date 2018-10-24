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

        damage(attacker, target, Mathf.RoundToInt(totalDamage), attributes.ToArray());
    }

	public void damage(GameObject Attacker,GameObject target, int amt, string[] attributes) {
        target.GetComponent<Health>().damage(amt);

        foreach (string attribute in attributes)
        {
            switch (attribute)
            {
                case "lifesteal":
                    // do something
                    break;
                case "burn":
                    // do something
                    break;
                case "splash":
                    // do something
                    break;
                // etc
            }
        }
	}
}
