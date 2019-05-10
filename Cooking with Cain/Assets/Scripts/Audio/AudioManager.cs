using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    static AudioManager instance  = null;
    public int audioValue;

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;

        audioSource = this.GetComponent<AudioSource>();
        //audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>(); ;
    }
}