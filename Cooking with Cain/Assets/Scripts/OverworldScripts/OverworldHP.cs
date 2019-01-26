using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OverworldHP : MonoBehaviour
{
    public TextMeshProUGUI hptext;
    public Image hpimage;
    public float currenthp=100;
    public float maxhp=100;

    // Start is called before the first frame update
    void Start()
    {
        hptext.GetComponentInChildren<TextMeshProUGUI>();

        if (PlayerPrefs.HasKey("MaxHP"))
        {
            maxhp = PlayerPrefs.GetInt("MaxHP");
        }
        if (PlayerPrefs.HasKey("CurrentHP"))
        {
            currenthp = PlayerPrefs.GetInt("CurrentHP");
        }
        UpdateDisplay();
    }

    // Update is called once per frame
    public void ChangeHP(float val)
    {
        currenthp += val;
        if (currenthp > maxhp)
        {
            currenthp = maxhp;
        }
        if (currenthp < 0)
        {
            currenthp = 0;
        }
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        hptext.text = currenthp.ToString();
        hpimage.fillAmount = (currenthp / maxhp);
    }
}
