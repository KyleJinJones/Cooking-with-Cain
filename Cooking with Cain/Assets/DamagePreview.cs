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
        text.text = string.Format(text.text, food.multiplierRange.Length == 2 ? Mathf.RoundToInt(food.multiplierRange[0] * attack).ToString() + "-" + Mathf.RoundToInt(food.multiplierRange[1] * attack).ToString() : Mathf.RoundToInt(food.multiplier * attack).ToString());
    }
}
