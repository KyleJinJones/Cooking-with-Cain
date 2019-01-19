using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public void PerformAttack(Entity attacker, Entity target, Entity[] targetTeam, Ingredient[] ingredients)
    {
        StartCoroutine(AttackAnimation(attacker.gameObject));

        float attack = attacker.stats.attack;

        int damageMin = 0;
        int damageMax = 0;

        List<Ingredient.Attribute> attributes = new List<Ingredient.Attribute>();

        foreach (Ingredient ingredient in ingredients)
        {
            switch (ingredient.damageType)
            {
                case Ingredient.DamageType.flat:
                    damageMin += Mathf.RoundToInt(attack * ingredient.multiplier);
                    damageMax += Mathf.RoundToInt(attack * ingredient.multiplier);
                    break;
                case Ingredient.DamageType.range:
                    damageMin += Mathf.RoundToInt(attack * ingredient.multiplierMin);
                    damageMax += Mathf.RoundToInt(attack * ingredient.multiplierMax);
                    break;
            }

            if (ingredient.attribute != Ingredient.Attribute.none)
            {
                attributes.Add(ingredient.attribute);
            }
        }
    }

    IEnumerator AttackAnimation(GameObject attacker)
    {
        float offset = attacker.tag.Equals("Player") ? 10f : -10f;

        for (int i = 0; i < 5; i++)
        {
            attacker.transform.SetPositionAndRotation(attacker.transform.position + new Vector3(offset, 0), attacker.transform.rotation);
            yield return null;
        }

        for (int i = 0; i < 5; i++)
        {
            attacker.transform.SetPositionAndRotation(attacker.transform.position - new Vector3(offset, 0), attacker.transform.rotation);
            yield return null;
        }
    }
}
