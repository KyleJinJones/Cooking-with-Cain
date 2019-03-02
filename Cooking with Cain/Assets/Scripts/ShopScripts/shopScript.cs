using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class shopScript : MonoBehaviour
{
    public static string currentScene;
    /*public static int gold;
    public static List<UpgradeInfo> items = new List<UpgradeInfo>();
    //public GameObject[] panels;
    public static string currentShop;
    
    private List<GameObject> children = new List<GameObject>();*/
    
    public List<ShopPanel> panels = new List<ShopPanel>();
    public Image[] slots = new Image[6];
    public int currentPanel = 0;

    private void Start()
    {
        panels = PanelHolder.setpanels;
    }

    private void Update() {
        ShopPanel panel = panels[currentPanel % panels.Count];

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < panel.items.Length)
            {
                slots[i].sprite = panel.items[i].upgradeimage;
                slots[i].GetComponent<ShopItem>().upgrade = panel.items[i];

                if (Gold.gold >= panel.items[i].goldcost && !SaveDataManager.currentData.shopBought.Contains(panel.items[i]))
                {
                    slots[i].color = Color.white;
                }
                else
                {
                    slots[i].color = Color.gray;
                }

                slots[i].gameObject.SetActive(true);
            }
            else
            {
                slots[i].GetComponent<ShopItem>().upgrade = null;
                slots[i].gameObject.SetActive(false);
            }
        }
        /*if (currentShop == "Upgrades") {
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
            
        }*/
    }

    // Start is called before the first frame update
    public void changeScene() {
        /*float xposition = PlayerPrefs.GetFloat("X") + 1.0f;
        float yposition = PlayerPrefs.GetFloat("Y");
        
        PlayerMovementFixed.spawnPosition = new Vector2(xposition, yposition);*/
        PlayerMovementFixed.checkpointPosition = PlayerMovementFixed.spawnPosition;
        SceneManager.LoadScene(currentScene);
    }
    
}

[System.Serializable]
public class ShopPanel
{
    public string name;
    public UpgradeInfo[] items = new UpgradeInfo[6];
}