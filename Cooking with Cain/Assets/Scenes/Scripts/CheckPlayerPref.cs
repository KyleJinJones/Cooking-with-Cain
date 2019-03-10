using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerPref : MonoBehaviour {
    public string keyname;
    public int keyvalue;
	// Used to restrict content the player does not have access to yet
	void Awake () {
        if (!(PlayerPrefs.HasKey(keyname) && PlayerPrefs.GetInt(keyname) == keyvalue))
        {
            this.gameObject.SetActive(false);
        }
	}
}
