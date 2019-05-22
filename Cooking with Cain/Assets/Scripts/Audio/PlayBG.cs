using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBG : MonoBehaviour
{
    public AudioClip bg;
    //Plays The BG Music Using AudioManager
    void Start()
    {
        AudioManager.instance.PlayBG(bg);
    }

    void Update()
    {
        
    }
}
