using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableIngredient : MonoBehaviour {
    public string named;
    
	// Use this for initialization
	void Start () {
        if ((PlayerPrefs.HasKey(named) && PlayerPrefs.GetInt(named) == 1))
        {
            this.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	public void Enableing () {
        PlayerPrefs.SetInt(named, 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("level") + 1);
    }
}
