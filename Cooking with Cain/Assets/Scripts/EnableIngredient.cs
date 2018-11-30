using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableIngredient : MonoBehaviour {
    public string name;
    
	// Use this for initialization
	void Start () {
        if ((PlayerPrefs.HasKey(name) && PlayerPrefs.GetInt(name) == 1))
        {
            this.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Enableing () {
        PlayerPrefs.SetInt(name, 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("level") + 1);
    }
}
