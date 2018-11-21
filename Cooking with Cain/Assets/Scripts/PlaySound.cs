using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
    public AudioSource asrc = null;
    public AudioClip cl;
	// Use this for initialization
	void Start () {
        if (asrc == null)
        {
            asrc = GetComponent<AudioSource>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
