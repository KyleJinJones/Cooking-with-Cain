using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    //Keeps track of the character's health
    public float starting_health = 100;
    public float health = 100;
    public float renderhealth = 100;
    public Text hptext;
    public Image hpbar;
    public int burnDuration;
    public float burnPercent;
    public Text damageIndicator;

	void Start () {
        health = renderhealth = starting_health;

        if (hptext != null)
            hptext.text = health.ToString();

        if (damageIndicator != null)
            damageIndicator.color = Color.clear;
	}

    void Update()
    {
        renderhealth += (health - renderhealth) / 5;

        if (hpbar != null)
            hpbar.fillAmount = renderhealth / starting_health;
    }

    public void updateHealth(float newHealth)
    {
        starting_health = newHealth;
        health = newHealth;

        if (hptext != null)
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
        StartCoroutine(healthIndicator("-" + amt, Color.red));
        health -= amt;

        if (health <= 0)
        {
            health = 0;
            StartCoroutine(die());
        }

        if (hptext != null)
            hptext.text = health.ToString();

    }

    IEnumerator die()
    {
        for (int i = 75; i > 0; i--)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 75f);
            transform.Rotate(0, 0, 15);
            transform.position = transform.position + new Vector3(0.5f, 0.2f);
            yield return null;
        }


        Destroy(gameObject);
    }

    public void heal(int amt)
    {
        StartCoroutine(healthIndicator("+" + amt, Color.green));
        health += amt;

        if (health > starting_health)
        {
            health = starting_health;
        }

        if (hptext != null)
            hptext.text = health.ToString();

    }

    IEnumerator healthIndicator(string amt, Color clr)
    {
        damageIndicator.text = amt;
        damageIndicator.color = clr;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        for (int i = 60; i > 0; i--)
        {
            if (clr.r == 1)
                renderer.color = new Color(1, clr.g + 3 - i / 20f, clr.g + 3 - i / 20f, 1);

            damageIndicator.color = new Color(clr.r, clr.g, clr.b, i / 60f);
            yield return null;
        }

        if (damageIndicator != null)
            damageIndicator.color = Color.clear;
    }
}
