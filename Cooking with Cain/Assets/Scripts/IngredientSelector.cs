using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class IngredientSelector : MonoBehaviour
{
    public Image[] images = new Image[3];

    new AudioSource audio = null;
    List<IngredientButton> selected = new List<IngredientButton>();

    void Start()
    {
        audio = GetComponent<AudioSource>();
        for (int i = 0; i < 3; i++)
        {
            if (images[i] != null)
            {
                images[i].color = Color.clear;
            }
        }
    }

    public bool Select(IngredientButton button)
    {
        audio.clip = button.ingredient.audioClip;
        audio.Play();

        if (selected.Contains(button))
        {
            selected.Remove(button);
            UpdateImages();
            return false;
        }
        else if (selected.Count < 3)
        {
            selected.Add(button);
            UpdateImages();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Clear()
    {
        selected.Clear();
        UpdateImages();
    }

    void UpdateImages()
    {
        for (int i = 0; i < 3; i++)
        {
            if (images[i] != null && selected.Count > i && selected[i] != null)
            {
                images[i].sprite = selected[i].ingredient.sprite;
                images[i].SetNativeSize();
                images[i].color = Color.white;
            }
            else
            {
                images[i].color = Color.clear;
            }
        }
    }
}
