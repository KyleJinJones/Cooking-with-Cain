using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlLocked : MonoBehaviour {
    public int lvl_unlocked;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("level") < lvl_unlocked)
        {
            this.gameObject.SetActive(false);
        }
	}

}
