using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectOnClick : MonoBehaviour {
    public int cmbnum;
    public GameObject manager =null;

    private void Start()
    {
        if (manager == null)
        {
            manager = GameObject.FindGameObjectWithTag("manager");
        }
    }
    // Use this for initialization
    void OnMouseDown () {
        Debug.Log("tryed");
        manager.GetComponent<Ingredient_Selection>().selected.RemoveAt(cmbnum);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
