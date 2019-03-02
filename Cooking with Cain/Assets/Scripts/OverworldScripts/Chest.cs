﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    public UpgradeInfo reward;
    public UpgradeInfo altReward;
    public Gold playergold;
    private bool open
    {
        get
        {
            return SaveDataManager.currentData.openedChests.Contains(chestId);
        }

        set
        {
            if (value && !open)
            {
                SaveDataManager.currentData.openedChests.Add(chestId);
            }
            else if (!value && open)
            {
                SaveDataManager.currentData.openedChests.Remove(chestId);
            }
        }
    }
    public GameObject treasurewindow;
    [SerializeField] private Sprite openview;
    public int id;
    private ChestId chestId;

    private void Awake()
    {
        chestId = new ChestId(SceneManager.GetActiveScene().name, id);
        if (open)
        {
            GetComponent<SpriteRenderer>().sprite = openview;
        }
    }

    // Provides the Player with an item upon them opening it.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!open&&collision.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            if (SaveDataManager.currentData.shopBought.Contains(reward))
            {
                altReward.obtain();
            }
            else
            {
                reward.obtain();

                if (reward.attributeType == UpgradeInfo.AttributeType.INGREDIENT)
                {
                    SaveDataManager.currentData.shopBought.Add(reward);
                }
            }

            open = true;
            treasurewindow.SetActive(true);
            Time.timeScale = 0;
            treasurewindow.GetComponent<TreasureWindow>().treasureimage.sprite = reward.upgradeimage;
            treasurewindow.GetComponent<TreasureWindow>().treasuretext.text = string.Format("Nice, You got {0}.", reward.rewardname);
            this.GetComponent<SpriteRenderer>().sprite = openview;

        }
    }

    public void AutoAssignAllChestId()
    {
        int nextId = 0;

        foreach (Chest despawner in FindObjectsOfType<Chest>())
        {
            Undo.RecordObject(despawner, "Auto Assign ID");
            despawner.id = nextId;
            nextId++;
        }
    }
}

[System.Serializable]
public class ChestId : System.IEquatable<ChestId>
{
    public string scene;
    public int id;

    public ChestId(string scene, int id)
    {
        this.scene = scene;
        this.id = id;
    }

    bool IEquatable<ChestId>.Equals(ChestId other)
    {
        return scene == other.scene && id == other.id;
    }
}

[CustomEditor(typeof(Chest))]
public class ChestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Auto Assign All Chest ID"))
        {
            ((Chest)target).AutoAssignAllChestId();
        }
    }
}