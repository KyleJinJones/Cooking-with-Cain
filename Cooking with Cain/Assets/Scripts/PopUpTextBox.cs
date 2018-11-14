using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTextBox : MonoBehaviour
{
    public GameObject textBox;

    void Start()
    {
        textBox.GetComponent<Renderer>().enabled = false;
    }

    public void OnMouseOver()
    {
        Debug.Log("MOUSE IS ON ME NUUU");
        textBox.GetComponent<Renderer>().enabled = true;
    }

    public void OnMouseExit()
    {
        textBox.GetComponent<Renderer>().enabled = false;
    }
}
