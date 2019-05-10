using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levers : MonoBehaviour
{
    public GameObject objectToDelete;
    public GameObject interactPopup;
    public GameObject txtwindow;
    public UpgradeInfo txt;
   [SerializeField]private Sprite pulled;

    bool activated
    {
        get
        {
            return SaveDataManager.currentData.activatedLevers.Contains(leverId);
        }
    }

    public int id;
    private LeverId leverId;

    // Start is called before the first frame update
    void Start()
    {
        txtwindow = GameObject.FindGameObjectWithTag("Popup");
        leverId = new LeverId(SceneManager.GetActiveScene().name, id);

        if (activated)
        {
            objectToDelete.SetActive(!objectToDelete.activeSelf);
            this.GetComponent<SpriteRenderer>().sprite = pulled;
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !activated)
        {
            interactPopup.SetActive(true);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.tag == "Player")
        {
            SaveDataManager.currentData.activatedLevers.Add(leverId);
            objectToDelete.SetActive(!objectToDelete.activeSelf);
            interactPopup.SetActive(false);
            txtwindow.SetActive(true);
            txtwindow.GetComponent<TreasureWindow>().treasureimage.sprite = txt.upgradeimage;
            txtwindow.GetComponent<TreasureWindow>().treasuretext.text = txt.infotext;
            this.GetComponent<SpriteRenderer>().sprite = pulled;

            if (GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>().Play();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !activated)
        {
            interactPopup.SetActive(false);
        }
    }
}

[System.Serializable]
public class LeverId : System.IEquatable<LeverId>
{
    public string scene;
    public int id;

    public LeverId(string scene, int id)
    {
        this.scene = scene;
        this.id = id;
    }

    bool System.IEquatable<LeverId>.Equals(LeverId other)
    {
        return scene == other.scene && id == other.id;
    }
}