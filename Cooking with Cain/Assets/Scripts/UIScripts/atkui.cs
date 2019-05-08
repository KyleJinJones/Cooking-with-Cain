using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class atkui : MonoBehaviour
{
    public static float attackpow
    {
        get
        {
            return SaveDataManager.currentData.playerStats.attack;
        }

        set
        {
            SaveDataManager.currentData.playerStats.attack = value;
        }
    }

    public TextMeshProUGUI attacktxt;

    // Start is called before the first frame update
    void Start()
    {
        attacktxt = GetComponent<TextMeshProUGUI>();
        attacktxt.text = string.Format("Attack:{0}", attackpow);

    }

    

    // Update is called once per frame
    void Update()
    {
        attacktxt.text = string.Format("Attack:{0}", attackpow);
    }
}
