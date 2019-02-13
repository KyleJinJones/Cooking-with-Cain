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

        attributes.Sort();

        StartCoroutine(PerformAttack(attacker, target, targetTeam, damageMin, damageMax, attributes));
    }

    IEnumerator PerformAttack(Entity attacker, Entity target, Entity[] targetTeam, int damageMin, int damageMax, List<Ingredient.Attribute> attributes)
    {
        attacker.StatusBlink(StatusInstance.Status.atkup, StatusInstance.Status.atkdown);
        ResultText.lines.Add(string.Format("{0} attacks", attacker.entityName));

        StartCoroutine(AttackAnimation(attacker.gameObject));
        int total = 0;
        int number = 0;

        float miss = attributes.Contains(Ingredient.Attribute.miss) ? attacker.stats.miss : 0;

        if (attributes.Contains(Ingredient.Attribute.splash))
        {
            if (Random.value < miss)
                ResultText.lines.Add(string.Format("The attack misses {0}", target.entityName));
            else
            {
                int damage = Random.Range(damageMin, damageMax);
                total += damage;

                number++;
                DealDamage(attacker, target, damage * attacker.stats.splash, attributes);
            }

            foreach (Entity enemy in targetTeam)
            {
                if (enemy != null && enemy != target)
                {
                    for (int i = 0; i < 5; i++)
                        yield return null;

                    if (Random.value < miss)
                        ResultText.lines.Add(string.Format("The attack misses {0}", enemy.entityName));
                    else
                    {
                        int damage = Random.Range(damageMin, damageMax);
                        total += damage;

                        number++;
                        DealDamage(attacker, enemy, damage * attacker.stats.splash, attributes);
                    }
                }
            }
        }
        else
        {
            if (Random.value < miss)
                ResultText.lines.Add(string.Format("The attack misses {0}", target.entityName));
            else
            {
                int damage = Random.Range(damageMin, damageMax);
                total += damage;

                number++;
                DealDamage(attacker, target, damage, attributes);
            }
        }

        foreach (Ingredient.Attribute attribute in attributes)
        {
            int value;

            switch (attribute)
            {
                case Ingredient.Attribute.leech:
                    if (number > 0)
                    {
                        value = Mathf.RoundToInt(total / number * attacker.stats.lifesteal);
                        attacker.ModifyHealth(value);
                        ResultText.lines.Add(string.Format("{0} gains {1} health", attacker.entityName, value));
                    }
                    break;
                case Ingredient.Attribute.atkup:
                    attacker.AddStatus(StatusInstance.Status.atkup, attacker.stats.atkboost, 2);
                    ResultText.lines.Add(string.Format("{0} gains {1}% attack boost", attacker.entityName, Mathf.RoundToInt(attacker.stats.atkboost * 100)));
                    break;
                case Ingredient.Attribute.defup:
                    attacker.AddStatus(StatusInstance.Status.defup, attacker.stats.defboost, 2);
                    ResultText.lines.Add(string.Format("{0} gains {1}% defense boost", attacker.entityName, Mathf.RoundToInt(attacker.stats.defboost * 100)));
                    break;
                case Ingredient.Attribute.reflect:
                    attacker.AddStatus(StatusInstance.Status.reflect, attacker.stats.reflect, 1);
                    ResultText.lines.Add(string.Format("{0} gains {1}% reflect", attacker.entityName, Mathf.RoundToInt(attacker.stats.reflect * 100)));
                    break;
                case Ingredient.Attribute.cleanse:
                    attacker.AddStatus(StatusInstance.Status.cleanse, 0, 2);
                    ResultText.lines.Add(string.Format("{0} gains debuff cleanse", attacker.entityName));

                    foreach (StatusInstance status in attacker.statuses)
                    {
                        switch (status.status)
                        {
                            case StatusInstance.Status.burn:
                                ResultText.lines.Add(string.Format("Burn is cleansed from {0}", attacker.entityName));
                                break;
                            case StatusInstance.Status.atkdown:
                                ResultText.lines.Add(string.Format("Attack debuff is cleansed from {0}", attacker.entityName));
                                break;
                            case StatusInstance.Status.stun:
                                ResultText.lines.Add(string.Format("Stun is cleansed from {0}", attacker.entityName));
                                break;
                            case StatusInstance.Status.defdown:
                                ResultText.lines.Add(string.Format("Defense debuff is cleansed from {0}", attacker.entityName));
                                break;
                        }
                    }

                    foreach (StatusInstance status in attacker.statuses.FindAll(status =>
                    status.status == StatusInstance.Status.burn ||
                    status.status == StatusInstance.Status.atkdown ||
                    status.status == StatusInstance.Status.stun ||
                    status.status == StatusInstance.Status.defdown))
                    {
                        status.duration = 0;
                    }

                    break;
                case Ingredient.Attribute.selfdmg:
                    value = Mathf.RoundToInt(attacker.stats.maxHealth * attacker.stats.selfdmg);
                    attacker.ModifyHealth(-value);
                    ResultText.lines.Add(string.Format("{0} takes {1} recoil damage", attacker.entityName, value));
                    break;
            }
        }
    }

    void DealDamage(Entity attacker, Entity target, float damage, List<Ingredient.Attribute> attributes)
    {
        target.StatusBlink(StatusInstance.Status.defup, StatusInstance.Status.defdown, StatusInstance.Status.reflect);
        int effectiveDamage = Mathf.RoundToInt(target.FactorDefense(damage));
        target.ModifyHealth(-effectiveDamage);
        ResultText.lines.Add(string.Format("{0} takes {1} damage", target.entityName, effectiveDamage));

        StatusInstance reflect = target.statuses.Find(status => status.status == StatusInstance.Status.reflect);

        if (reflect != null)
        {
            int spike = Mathf.RoundToInt(damage * reflect.potency);
            attacker.ModifyHealth(-spike);
            ResultText.lines.Add(string.Format("{0} damage is reflected back to {1}", spike, attacker.entityName));
        }

        if (target.statuses.Exists(status => status.status == StatusInstance.Status.cleanse))
        {
            if (attributes.FindAll(attribute =>
            attribute == Ingredient.Attribute.burn ||
            attribute == Ingredient.Attribute.atkdown ||
            attribute == Ingredient.Attribute.stun ||
            attribute == Ingredient.Attribute.defdown).Count > 0)
            {
                target.StatusBlink(StatusInstance.Status.cleanse);
            }
        }
        else
        {
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
                    case Ingredient.Attribute.defdown:
                        target.AddStatus(StatusInstance.Status.defdown, attacker.stats.defdebuff, 2);
                        ResultText.lines.Add(string.Format("{0} gets {1}% defense reduction", target.entityName, Mathf.RoundToInt(attacker.stats.defdebuff * 100)));
                        break;
                }
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
