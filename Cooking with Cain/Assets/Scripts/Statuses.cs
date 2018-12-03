using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuses : MonoBehaviour {
    public Turn_Manager turnManager;
    public GameObject burnIcon;
    public GameObject attackBoostIcon;
    public GameObject attackDebuffIcon;
    public GameObject stunIcon;

    GameObject player;
    GameObject[] playerIcons;
    GameObject[][] enemyIcons = new GameObject[3][];

	// Use this for initialization
	void Start () {
        player = turnManager.getPlayers()[0];
        playerIcons = new GameObject[] {
            Object.Instantiate(burnIcon, new Vector3(-8, 2.5f), Quaternion.identity, burnIcon.transform.parent),
            Object.Instantiate(attackBoostIcon, new Vector3(-6, 2.5f), Quaternion.identity, attackBoostIcon.transform.parent),
            Object.Instantiate(attackDebuffIcon, new Vector3(-4, 2.5f), Quaternion.identity, attackBoostIcon.transform.parent)};

        Vector3[] positions = turnManager.enemyPositions;

        enemyIcons = new GameObject[][] {
            new GameObject[] {
            Object.Instantiate(burnIcon, positions[0] + new Vector3(-1.5f, 2), Quaternion.identity, burnIcon.transform.parent),
            Object.Instantiate(attackBoostIcon, positions[0] + new Vector3(-0.5f, 2), Quaternion.identity, attackBoostIcon.transform.parent),
            Object.Instantiate(attackDebuffIcon, positions[0] + new Vector3(0.5f, 2), Quaternion.identity, attackBoostIcon.transform.parent),
            Object.Instantiate(stunIcon, positions[0] + new Vector3(1.5f, 2), Quaternion.identity, stunIcon.transform.parent)},
            new GameObject[] {
            Object.Instantiate(burnIcon, positions[1] + new Vector3(-1.5f, 2), Quaternion.identity, burnIcon.transform.parent),
            Object.Instantiate(attackBoostIcon, positions[1] + new Vector3(-0.5f, 2), Quaternion.identity, attackBoostIcon.transform.parent),
            Object.Instantiate(attackDebuffIcon, positions[1] + new Vector3(0.5f, 2), Quaternion.identity, attackBoostIcon.transform.parent),
            Object.Instantiate(stunIcon, positions[1] + new Vector3(1.5f, 2), Quaternion.identity, stunIcon.transform.parent)},
            new GameObject[] {
            Object.Instantiate(burnIcon, positions[2] + new Vector3(-1.5f, 2), Quaternion.identity, burnIcon.transform.parent),
            Object.Instantiate(attackBoostIcon, positions[2] + new Vector3(-0.5f, 2), Quaternion.identity, attackBoostIcon.transform.parent),
            Object.Instantiate(attackDebuffIcon, positions[2] + new Vector3(0.5f, 2), Quaternion.identity, attackBoostIcon.transform.parent),
            Object.Instantiate(stunIcon, positions[2] + new Vector3(1.5f, 2), Quaternion.identity, stunIcon.transform.parent)}
        };
    }
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            for (int i = 0; i < 3; i++)
            {
                playerIcons[i].SetActive(false);
            }
        }
        else
        {
            bool[] render = getStatuses(player);

            for (int i = 0; i < 3; i++)
            {
                playerIcons[i].SetActive(render[i]);
            }
        }

        GameObject[] enemies = turnManager.enemies;

        for (int j = 0; j < 3; j++)
        {
            if (enemies[j] == null || enemies[j].GetComponent<Health>().health == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    enemyIcons[j][i].SetActive(false);
                }
            }
            else
            {
                bool[] render2 = getStatusesEnemy(enemies[j]);

                for (int i = 0; i < 4; i++)
                {
                    enemyIcons[j][i].SetActive(render2[i]);
                }
            }
        }
    }

    bool[] getStatuses(GameObject entity)
    {
        return new bool[] { entity.GetComponent<Health>().burnDuration > 0, entity.GetComponent<Attack>().boostDuration > 0, entity.GetComponent<Attack>().debuffDuration > 0 };
    }

    bool[] getStatusesEnemy(GameObject entity)
    {
        return new bool[] { entity.GetComponent<Health>().burnDuration > 0, entity.GetComponent<Attack>().boostDuration > 0, entity.GetComponent<Attack>().debuffDuration > 0, entity.GetComponent<Enemy_Turn>().stunned };
    }
}
