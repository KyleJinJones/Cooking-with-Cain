using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUpdater : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex);
	}
	
}
