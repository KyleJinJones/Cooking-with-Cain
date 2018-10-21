using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deal_Damage : MonoBehaviour {

	public void damage(GameObject Attacker,GameObject target, int amt, string[] attributes) {
        target.GetComponent<Health>().damage(amt);

	}
}
