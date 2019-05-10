using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundMenu : MonoBehaviour
{
    public Slider sfxSlider;
    public TMP_Text sfxValue;
    public AudioManager audioManager;

    // Update is called once per frame
    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        sfxValue.text = sfxSlider.value.ToString() + " :";
        audioManager.audioValue = (int)sfxSlider.value;
    }
}
