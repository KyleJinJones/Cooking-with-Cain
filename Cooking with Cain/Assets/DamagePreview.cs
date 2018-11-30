using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePreview : MonoBehaviour {
    float attack;
    public Food food;

	// Use this for initialization
	void Start () {
        attack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>().attack;
        Text text = GetComponent<Text>();
        
        if (food.attribute.Equals(""))
            text.text = string.Format(text.text, food.multiplierRange.Length == 2 ? Mathf.RoundToInt(food.multiplierRange[0] * attack).ToString() + "-" + Mathf.RoundToInt(food.multiplierRange[1] * attack).ToString() : Mathf.RoundToInt(food.multiplier * attack).ToString());
        else
        {
            AttributeStats stats = GameObject.FindGameObjectWithTag("Player").GetComponent<AttributeStats>();
            string percent = "";

            if (food.attribute.Equals("burn"))
                percent = Mathf.RoundToInt(stats.burn * 100).ToString();
            else if (food.attribute.Equals("splash"))
                percent = Mathf.RoundToInt(stats.splash * 100).ToString();
            else if (food.attribute.Equals("lifesteal"))
                percent = Mathf.RoundToInt(stats.lifesteal * 100).ToString();
            else if (food.attribute.Equals("atkboost"))
                percent = Mathf.RoundToInt(stats.atkboost * 100).ToString();
            else if (food.attribute.Equals("atkdebuff"))
                percent = Mathf.RoundToInt(stats.atkdebuff * 100).ToString();
            else if (food.attribute.Equals("stun"))
                percent = Mathf.RoundToInt(stats.stun).ToString();

            text.text = string.Format(text.text, food.multiplierRange.Length == 2 ? Mathf.RoundToInt(food.multiplierRange[0] * attack).ToString() + "-" + Mathf.RoundToInt(food.multiplierRange[1] * attack).ToString() : Mathf.RoundToInt(food.multiplier * attack).ToString(), percent);
        }
    }
}
