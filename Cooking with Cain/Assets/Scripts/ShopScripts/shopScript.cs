using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class shopScript : MonoBehaviour
{
    public static string currentScene;
    public static int gold;
    public static List<UpgradeInfo> items = new List<UpgradeInfo>();
    public GameObject[] panels;
    public static string currentShop;
    
    private List<GameObject> children = new List<GameObject>();
    
    public void Start() {
        panels = GameObject.FindGameObjectsWithTag("ShopPanels");

        
        foreach (GameObject panel in panels) {
            foreach (UpgradeInfo item in items) {
                if (panel.name == item.attributename) {
                    panel.gameObject.SetActive(true);
                    break;
                } else {
                    panel.gameObject.SetActive(false);
                }
            }
        }
    }
    
    public void Update() {
        if (currentShop == "Upgrades") {
            foreach(GameObject panel in panels) {
                foreach(UpgradeInfo item in items) {
                    if (panel.name == item.attributename) {
                        if (item.attributetype != "a") {
                            panel.gameObject.SetActive(false);
                        } else {
                            panel.gameObject.SetActive(true);
                        }
                    }
                    
                }
            }
        } else if (currentShop == "Ingredients") {
            foreach(GameObject panel in panels) {
                foreach(UpgradeInfo item in items) {
                    if (panel.name == item.attributename) {
                        if (item.attributetype != "f") {
                            panel.gameObject.SetActive(false);
                        } else if (item.inInventory == true) {
                            Debug.Log("Wop" + item.attributename);
                            panel.gameObject.SetActive(false);
                        } else {
                            panel.gameObject.SetActive(true);
                        }
                    }
                    
                }
            }
            
        } else if (currentShop == "Items") {
            foreach(GameObject panel in panels) {
                foreach(UpgradeInfo item in items) {
                    if (panel.name == item.attributename) {
                        if (item.attributetype != "i") {
                            panel.gameObject.SetActive(false);
                        } else {
                            panel.gameObject.SetActive(true);
                        }
                    }
                    
                }
            }
            
        }
    }
    
    // Start is called before the first frame update
    public void changeScene() {
        float xposition = PlayerPrefs.GetFloat("X") + 1.0f;
        float yposition = PlayerPrefs.GetFloat("Y");
        
        PlayerMovementFixed.spawnPosition = new Vector2(xposition, yposition);
        
        SceneManager.LoadScene(currentScene);
    }
    
}
