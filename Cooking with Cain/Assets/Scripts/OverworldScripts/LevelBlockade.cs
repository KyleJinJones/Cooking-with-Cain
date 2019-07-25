using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlockade : MonoBehaviour
{
    // Start is called before the first frame update
    public int reqlevel = 0;
    void Start()
    {
        if (SaveDataManager.currentData.level>=reqlevel)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
