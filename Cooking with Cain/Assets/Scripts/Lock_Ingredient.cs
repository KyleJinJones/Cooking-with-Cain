using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lock_Ingredient : MonoBehaviour {
    public int available_scene=0;
	// Use this for initialization
	void Start () {
		if (SceneManager.GetActiveScene().buildIndex < available_scene)
        {
            this.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
