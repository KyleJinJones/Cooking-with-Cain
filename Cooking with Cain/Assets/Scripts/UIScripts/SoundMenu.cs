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
    public TMP_Text bgmValue;
    public Slider bgmSlider;

    // Update is called once per frame
    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        sfxSlider.value = audioManager.sfxaudio.volume * 100;
        bgmSlider.value = audioManager.sfxaudio.volume * 100;
        sfxValue.text = sfxSlider.value + " :";
        bgmValue.text = bgmSlider.value + " :";

    }

    void Update()
    {
       // sfxValue.text = sfxSlider.value.ToString() + " :";
        //audioManager.bgvol = (float)sfxSlider.value/2.0f;
        //audioManager.sfxvol = (float)sfxSlider.value;
    }

    public void Updatesfxvol()
    {
        updatevol(sfxSlider.value, audioManager.sfxaudio, sfxValue);
    }

    public void Updatebgmvol()
    {
        updatevol(bgmSlider.value, audioManager.bgaudio, bgmValue);
    }

    void updatevol(float volumeval, AudioSource aud,TMP_Text t)
    {
        t.text = volumeval+" :";
        aud.volume = volumeval/100;
    }

}
