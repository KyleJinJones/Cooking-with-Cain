using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTransition : MonoBehaviour
{

    public GameObject interactPopup;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactPopup.SetActive(true);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.tag == "Player")
        {
            GetComponent<BossEncounter>().StartEncounter(collision.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactPopup.SetActive(false);
        }
    }
}

    
