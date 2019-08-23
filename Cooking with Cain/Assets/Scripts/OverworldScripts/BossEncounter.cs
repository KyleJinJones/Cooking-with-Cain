using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEncounter : EnemyEncounter
{
    public List<GameObject> bosses = new List<GameObject>();
    public string preBattleScene;
    public string winScene;

    public override void StartEncounter(GameObject player)
    {
        EnemyLoader.bosses = bosses;
        
        // Copied from EnemyEncounter
        EnemyLoader.enemies = enemies;
        PlayerMovementFixed.spawnPosition = player.transform.position;
        EntityManager.overworldScene = SceneManager.GetActiveScene().name;
        EntityManager.overworldSceneLose = SceneManager.GetActiveScene().name;
        EnemyDespawner despawner;
        if ((despawner = GetComponent<EnemyDespawner>()) != null)
            EnemyDespawner.despawned.Add(despawner.enemyID);

        if (preBattleScene != "")
            SceneManager.LoadScene(preBattleScene);
        else
            SceneManager.LoadScene("Battle");

        if (winScene != "")
            EntityManager.overworldScene = winScene;
        WinScene.lastScene = SceneManager.GetActiveScene().name;
    }
}