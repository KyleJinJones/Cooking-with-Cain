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
}
