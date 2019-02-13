using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeInfo.asset", menuName = "Cooking with Cain/UpgradeInfo")]
public class UpgradeInfo : ScriptableObject
{
    public string attributetype;
    public string attributename;
    public string infotext;
    public int goldcost;
    public float upgradeamt;
    public Sprite upgradeimage;
}
