using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    List<Ingredient> lastPlayed = new List<Ingredient>();

    public void ProcessAttack(Entity attacker, Entity target, Entity[] targetTeam, Ingredient[] ingredients)
    {
        float attack = attacker.GetEffectiveAttack();

        if (attacker.tag == "Player")
        {
            float mod = 0.8f;

            foreach (Ingredient ingredient in ingredients)
                if (!lastPlayed.Contains(ingredient))
                    mod = 1;

            attack *= mod;

            lastPlayed.Clear();
            lastPlayed.AddRange(ingredients);
        }

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

        StartCoroutine(PerformAttack(attacker, target, targetTeam, damageMin, damageMax, attributes));
    }

    IEnumerator PerformAttack(Entity attacker, Entity target, Entity[] targetTeam, int damageMin, int damageMax, List<Ingredient.Attribute> attributes)
    {
        ResultText.lines.Add(string.Format("{0} attacks", attacker.entityName));

        StartCoroutine(AttackAnimation(attacker.gameObject));
        int total = Random.Range(damageMin, damageMax);
        int number = 1;

        if (attributes.Contains(Ingredient.Attribute.splash))
        {
            DealDamage(attacker, target, Mathf.RoundToInt(total * attacker.stats.splash), attributes);

            foreach (Entity enemy in targetTeam)
            {
                if (enemy != null && enemy != target)
                {
                    for (int i = 0; i < 5; i++)
                        yield return null;

                    int damage = Random.Range(damageMin, damageMax);
                    total += damage;

                    number++;
                    DealDamage(attacker, enemy, Mathf.RoundToInt(damage * attacker.stats.splash), attributes);
                }
            }
        }
        else
        {
            DealDamage(attacker, target, total, attributes);
        }

        foreach (Ingredient.Attribute attribute in attributes)
        {
            switch (attribute)
            {
                case Ingredient.Attribute.leech:
                    attacker.ModifyHealth(Mathf.RoundToInt(total / number));
                    ResultText.lines.Add(string.Format("{0} gains {1} health", attacker.entityName, Mathf.RoundToInt(total / number)));
                    break;
                case Ingredient.Attribute.atkup:
                    attacker.AddStatus(StatusInstance.Status.atkup, attacker.stats.atkboost, 2);
                    ResultText.lines.Add(string.Format("{0} gains {1}% attack boost", attacker.entityName, Mathf.RoundToInt(attacker.stats.atkboost * 100)));
                    break;
            }
        }
    }

    void DealDamage(Entity attacker, Entity target, int damage, List<Ingredient.Attribute> attributes)
    {
        target.ModifyHealth(-damage);
        ResultText.lines.Add(string.Format("{0} takes {1} damage", target.entityName, damage));

        foreach (Ingredient.Attribute attribute in attributes)
        {
            switch (attribute)
            {
                case Ingredient.Attribute.burn:
                    target.AddStatus(StatusInstance.Status.burn, attacker.stats.burn, 3);
                    ResultText.lines.Add(string.Format("{0} gets {1}% burned", target.entityName, Mathf.RoundToInt(attacker.stats.burn * 100)));
                    break;
                case Ingredient.Attribute.atkdown:
                    target.AddStatus(StatusInstance.Status.atkdown, attacker.stats.atkdebuff, 3);
                    ResultText.lines.Add(string.Format("{0} gets {1}% attack reduction", target.entityName, Mathf.RoundToInt(attacker.stats.atkdebuff * 100)));
                    break;
                case Ingredient.Attribute.stun:
                    if (Random.value < attacker.stats.stun * (target.stunnedLastTurn ? 0.5f : 1))
                    {
                        target.AddStatus(StatusInstance.Status.stun, 0, 1);
                        ResultText.lines.Add(string.Format("{0} gets stunned", target.entityName));
                    }
                    break;
            }
        }
    }

    IEnumerator AttackAnimation(GameObject attacker)
    {
        float offset = attacker.tag.Equals("Player") ? 10f : -10f;

        for (int i = 0; i < 5; i++)
        {
            attacker.transform.localPosition = attacker.transform.localPosition + new Vector3(offset, 0);
            yield return null;
        }

        for (int i = 0; i < 5; i++)
        {
            attacker.transform.localPosition = attacker.transform.localPosition - new Vector3(offset, 0);
            yield return null;
        }
    }
}
