using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enterShop : MonoBehaviour
{
    public static int gold;
    public List<ShopPanel> items ;
    public GameObject shopEnterPanel;
    
    private bool changeShop;
    
    private void Start() {
        shopEnterPanel.gameObject.SetActive(false);
        changeShop = false;
    }
    
    
    public void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == "Player")
        {
            shopEnterPanel.gameObject.SetActive(true);
            changeShop = true;

            
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.tag == "Player")
        {
            shopScript.currentScene = SceneManager.GetActiveScene().name;

            PlayerMovementFixed.spawnPosition = collision.transform.position;
            PanelHolder.setpanels = items;
            SceneManager.LoadScene("Shop");

        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        shopEnterPanel.gameObject.SetActive(false);
        changeShop = false;
    }
    
    /*private void Update() {
        if (changeShop) {
            if (Input.GetKeyDown(KeyCode.E)) {
                /*shopEnterPanel.gameObject.SetActive(false);
                PlayerPrefs.SetFloat("X",player.transform.position.x);
                PlayerPrefs.SetFloat("Y",player.transform.position.y);

                shopScript.items.Clear();
                shopScript.items = items;

                shopScript.currentScene = SceneManager.GetActiveScene().name;

                PlayerMovementFixed.spawnPosition = player.transform.position;

                SceneManager.LoadScene("Shop");
            }
        }
    }*/
}
