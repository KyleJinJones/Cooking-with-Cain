using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<UpgradeInfo> upgrades;
    [SerializeField] private List<UpgradeStats> upgradebuttons;
    private int index =0;
    void Start()
    {
        foreach(UpgradeStats button in upgradebuttons)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switchupgrades(int dir)
    {
        index += dir;
        if (index < 0)
        {
            index = 0;
        }
        else
        {
            
        }
    }
}
