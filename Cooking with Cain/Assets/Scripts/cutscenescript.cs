using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutscenescript : MonoBehaviour {

    public GameObject dmanager;
    public DialogueManager d;
    public int sentcount;
    public GameObject bsprouts;
	// Use this for initialization
	void Start () {
        bsprouts.SetActive(false);
        d = dmanager.GetComponent<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (d.sentences.Count == sentcount)
        {
            bsprouts.SetActive(true);
            

        }
	}
}
