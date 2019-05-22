using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource sfxaudio;
    public AudioSource bgaudio;
    public static AudioManager instance  = null;
    public float bgvol=.5f;
    public float sfxvol=1.0f;


    // Start is called before the first frame update
     void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;

        //audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>(); ;
    }

    public void PlayBG(AudioClip a)
    {
        bgaudio.loop = true;
        bgaudio.volume = bgvol;
        bgaudio.clip = a;
        bgaudio.Play();
    }

    public void PlaySFX(AudioClip a)
    {
        sfxaudio.volume = sfxvol;
        sfxaudio.PlayOneShot(a);
    }

  
        
    

    
}