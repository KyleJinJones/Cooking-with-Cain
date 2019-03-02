using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levers : MonoBehaviour
{
    public GameObject objectToDelete;
    public GameObject interactPopup;

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
        leverId = new LeverId(SceneManager.GetActiveScene().name, id);

        if (activated)
        {
            objectToDelete.SetActive(false);
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
            objectToDelete.SetActive(false);
            interactPopup.SetActive(false);
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