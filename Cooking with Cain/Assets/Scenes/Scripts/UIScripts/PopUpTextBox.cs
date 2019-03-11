using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTextBox : MonoBehaviour
{
    public GameObject textBox;

    void Start()
    {
        textBox.SetActive(false);
    }

    public void OnMouseOver()
    {
        textBox.SetActive(true);
    }

    public void OnMouseExit()
    {
        textBox.SetActive(false);
    }
}
