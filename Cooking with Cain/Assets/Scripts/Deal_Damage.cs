using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deal_Damage : MonoBehaviour {

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
                case "stun":
                    // do something
                    break;
                // etc
            }
        }
	}
}
