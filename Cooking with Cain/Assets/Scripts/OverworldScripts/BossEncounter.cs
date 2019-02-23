using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEncounter : EnemyEncounter
{
    public List<GameObject> bosses = new List<GameObject>();

    public override void StartEncounter(GameObject player)
    {
        EnemyLoader.bosses = bosses;
        base.StartEncounter(player);
    }
}