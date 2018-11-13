using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public float attack = 10;
    public int boostDuration = 0;
	public int debuffDuration = 0;
	public float debuffPercent = 0;
    public List<string> lastPlayed;

    public float UpdateTurn()
    {
		float multiplier = 1;
        if (boostDuration > 0)
        {
            boostDuration--;
            multiplier += GetComponent<AttributeStats>().atkboost;
        }
		
		if (debuffDuration > 0)
		{
			debuffDuration--;
			multiplier -= debuffPercent;
		}
		else
		{
			debuffPercent = 0;
		}
        return attack * multiplier;
        
    }
}
