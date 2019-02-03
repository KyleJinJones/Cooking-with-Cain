using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HPpotion : MonoBehaviour
{
    public float healamt=25;
    public OverworldHP hp;
    public int amtowned = 0;
    public TextMeshProUGUI ownedtxt;

    private void Start()
    {
        if (PlayerPrefs.HasKey(this.name))
        {
            amtowned = PlayerPrefs.GetInt(this.name);
        }
        else
        {
            PlayerPrefs.SetInt(this.name, amtowned);
        }
        ownedtxt = GetComponentInChildren<TextMeshProUGUI>();
        UpdateAmtOwned();
    }
    public void OnUse()
    {
        if (amtowned > 0)
        {
            hp.ChangeHP(healamt);
            amtowned -= 1;
            UpdateAmtOwned();
        }
    }

    private void UpdateAmtOwned()
    {
        ownedtxt.text = amtowned.ToString();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(this.name, amtowned);
    }
}
