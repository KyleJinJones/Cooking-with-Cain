using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public int attack = 10;
    public int boostDuration = 0;

    public int UpdateTurn()
    {
        if (boostDuration > 0)
        {
            boostDuration--;
            return Mathf.RoundToInt(attack * 1.2f);
        }

        return attack;
    }
}
