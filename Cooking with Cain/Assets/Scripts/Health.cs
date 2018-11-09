using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    //Keeps track of the character's health
    public float starting_health = 100;
    public float health = 100;
    public Text hptext;
    public Image hpbar;
    public int burnDuration;
    public float burnPercent;

	void Start () {
        health = starting_health;
        hptext.text = health.ToString();
	}
	
    public void updateHealth(float newHealth)
    {
        starting_health = newHealth;
        health = newHealth;
        hptext.text = health.ToString();
    }

    public void UpdateTurn()
    {
        if (burnDuration > 0)
        {
            damage(Mathf.RoundToInt(Mathf.Max(health * burnPercent, 1)));
            burnDuration--;

            if (burnDuration == 0)
            {
                burnPercent = 0;
            }
        }
        else
        {
            burnPercent = 0;
        }
    }

	//Takes an amount of damage, and updates its healthbar
	public void  damage(int amt) {
        health -= amt;

        if (health <= 0)
        {
            health = 0;
            StartCoroutine(die());
        }


        hptext.text = health.ToString();
        hpbar.fillAmount = health / starting_health;

    }

    IEnumerator die()
    {
        for (int i = 60; i > 0; i--)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 60f);
            yield return null;
        }

        Destroy(gameObject);
    }

    public void heal(int amt)
    {
        health += amt;

        if (health > starting_health)
        {
            health = starting_health;
        }

        hptext.text = health.ToString();
        hpbar.fillAmount = health / starting_health;

    }
}
