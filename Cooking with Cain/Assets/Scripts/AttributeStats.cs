﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeStats : MonoBehaviour
{
    public float burn = 0.07f;
    public float splash = 0.5f;
    public float lifesteal = 0.5f;
    public float atkboost = 0.2f;

    public void Start()
    {
        if(this.gameObject.tag == "Player")
        {
            Debug.Log("Starting Attributes");
            Debug.Log("Lifesteal: " + lifesteal);
            if (!PlayerPrefs.HasKey("health"))
            {

                PlayerPrefs.SetFloat("health", 100.0f);    
            }
            else
            {
                GetComponent<Health>().updateHealth(PlayerPrefs.GetFloat("health"));
            }

            if (!PlayerPrefs.HasKey("splash"))
            {
                PlayerPrefs.SetFloat("splash", 0.05f);
            }
            else
            {
                splash = PlayerPrefs.GetFloat("splash");
            }

            if (!PlayerPrefs.HasKey("lifesteal"))
            {
                PlayerPrefs.SetFloat("lifesteal", 0.5f);
            }
            else
            {
                lifesteal = PlayerPrefs.GetFloat("lifesteal");
            }

            if (!PlayerPrefs.HasKey("atkboost"))
            {
                PlayerPrefs.SetFloat("atkboost", 0.2f);
            }
            else
            {
                atkboost = PlayerPrefs.GetFloat("atkboost");
            }
            Debug.Log("New Lifesteal: " + lifesteal);
        }
        
    }

}