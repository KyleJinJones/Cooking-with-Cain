using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    public UpgradeInfo reward;
    public UpgradeInfo altReward;
    public Gold playergold;
    public AudioManager audioManager;
    private bool open
    {
        get
        {
            return SaveDataManager.currentData.openedChests.Contains(chestId);
        }

        set
        {
            if (value && !open)
            {
                SaveDataManager.currentData.openedChests.Add(chestId);
            }
            else if (!value && open)
            {
                SaveDataManager.currentData.openedChests.Remove(chestId);
            }
        }
    }
    public GameObject treasurewindow;
    [SerializeField] private Sprite openview;
    public int id;
    private ChestId chestId;
    
    public GameObject chestOpenPanel;

    private void Awake()
    {
        treasurewindow = GameObject.FindGameObjectWithTag("Popup");
        chestOpenPanel = GameObject.FindGameObjectWithTag("Interact");
        chestId = new ChestId(SceneManager.GetActiveScene().name, id);
        if (open)
        {
            GetComponent<SpriteRenderer>().sprite = openview;
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        chestOpenPanel.gameObject.SetActive(false);
        treasurewindow.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if ((collision.gameObject.tag == "Player") && (!open)) {
            chestOpenPanel.gameObject.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision) {
        chestOpenPanel.gameObject.SetActive(false);
    }

    // Provides the Player with an item upon them opening it.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!open&&collision.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            chestOpenPanel.gameObject.SetActive(false);
            if (SaveDataManager.currentData.shopBoughtIngredient.Contains(reward))
            {
                altReward.obtain();

                
                if (altReward.rewardname != " ")
                {
                    treasurewindow.GetComponent<TreasureWindow>().treasureimage.sprite = altReward.upgradeimage;
                    treasurewindow.GetComponent<TreasureWindow>().treasuretext.text = string.Format("Nice, You got {0}.", altReward.rewardname);
                }
            }
            else if (reward.attributeType==UpgradeInfo.AttributeType.TEXT)
            {
                treasurewindow.GetComponent<TreasureWindow>().treasureimage.sprite = reward.upgradeimage;
                treasurewindow.GetComponent<TreasureWindow>().treasuretext.text = reward.infotext;
            }
            else
            {
                reward.obtain();

                if (reward.attributeType == UpgradeInfo.AttributeType.INGREDIENT)
                {
                    SaveDataManager.currentData.shopBoughtIngredient.Add(reward);
                }

                if (reward.rewardname != " ")
                {
                    treasurewindow.GetComponent<TreasureWindow>().treasureimage.sprite = reward.upgradeimage;
                    treasurewindow.GetComponent<TreasureWindow>().treasuretext.text = string.Format("Nice, You got {0}.", reward.rewardname);
                }
            }

            if (reward.attributeType != UpgradeInfo.AttributeType.TEXT)
            {
                open = true;
            }
            treasurewindow.SetActive(true);
            Time.timeScale = 0;
            
            
            
            this.GetComponent<SpriteRenderer>().sprite = openview;

            if (GetComponent<AudioSource>() != null) {
                this.GetComponent<AudioSource>().volume = audioManager.audioValue;
                GetComponent<AudioSource>().Play();
            }
        }
    }
}

[System.Serializable]
public class ChestId : System.IEquatable<ChestId>
{
    public string scene;
    public int id;

    public ChestId(string scene, int id)
    {
        this.scene = scene;
        this.id = id;
    }

    bool IEquatable<ChestId>.Equals(ChestId other)
    {
        return scene == other.scene && id == other.id;
    }
}