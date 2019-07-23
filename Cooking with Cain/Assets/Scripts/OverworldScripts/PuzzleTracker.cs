using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTracker : MonoBehaviour
{
    public GameObject puzzleobj;
    public int puzzleid;
    
    // Start is called before the first frame update
    void Start()
    {
        if (SaveDataManager.currentData.puzzles.Contains(puzzleid))
        {
            puzzleobj.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!SaveDataManager.currentData.puzzles.Contains(puzzleid))
        {
            SaveDataManager.currentData.puzzles.Add(puzzleid);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
