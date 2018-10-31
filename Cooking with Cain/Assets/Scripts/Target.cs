using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour {

    public GameObject player;
    public GameObject indicator;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    private void OnMouseDown()
    {
        player.GetComponent<Player_Turn>().target = this.gameObject;
    }
    void Update () {
        if (player.GetComponent<Player_Turn>().target == this.gameObject)
        {
            indicator.SetActive(true);
        }
        else
        {
            indicator.SetActive(false);
        }
	}
}
