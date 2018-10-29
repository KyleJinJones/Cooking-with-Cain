using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public GameObject player;
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
		
	}
}
