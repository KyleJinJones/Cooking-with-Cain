using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class IngredientSelector : MonoBehaviour
{
    public EntityManager manager;
    public Image[] images = new Image[3];
    public Button attackButton;

    new AudioSource audio = null;

    List<IngredientButton> selected = new List<IngredientButton>();

    TooltipText results;

    void Start()
    {
        results = attackButton.gameObject.AddComponent<TooltipText>();

        audio = GetComponent<AudioSource>();
        for (int i = 0; i < 3; i++)
        {
            if (images[i] != null)
            {
                images[i].color = Color.clear;
            }
        }

        UpdateImages();
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

    public void Deselect(int index)
    {
        selected[index].Deselect();
        selected.RemoveAt(index);
        UpdateImages();
    }

    public void Clear()
    {
        foreach (IngredientButton button in selected)
        {
            button.Deselect();
        }

        selected.Clear();
        UpdateImages();
    }

    void UpdateImages()
    {
        if (selected.Count == 3)
        {
            attackButton.interactable = true;
            results.text = GetIngredientResult();
        }
        else
        {
            attackButton.interactable = false;
            results.text = null;
        }

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

    public void LockIn()
    {
        if (selected.Count == 3)
        {
            manager.PlayerAction(selected.ConvertAll(button => button.ingredient).ToArray());
            Clear();
        }
    }

    string GetIngredientResult()
    {
        Stats stats = Entity.playerStats;

        float attack = stats.attack;

        int damageMin = 0;
        int damageMax = 0;

        List<Ingredient.Attribute> attributes = new List<Ingredient.Attribute>();

        foreach (Ingredient ingredient in selected.ConvertAll(button => button.ingredient))
        {
            switch (ingredient.damageType)
            {
                case Ingredient.DamageType.flat:
                    damageMin += Mathf.RoundToInt(attack * ingredient.multiplier);
                    damageMax += Mathf.RoundToInt(attack * ingredient.multiplier);
                    break;
                case Ingredient.DamageType.range:
                    damageMin += Mathf.RoundToInt(attack * ingredient.multiplierMin);
                    damageMax += Mathf.RoundToInt(attack * ingredient.multiplierMax);
                    break;
            }

            if (ingredient.attribute != Ingredient.Attribute.none)
            {
                attributes.Add(ingredient.attribute);
            }
        }

        string tooltip = "Total damage: ";

        if (damageMin == damageMax)
            tooltip += damageMin;
        else
            tooltip += string.Format("{0}~{1}", damageMin, damageMax);

        if (attributes.Count > 0)
        {
            tooltip += "\nEffects:";
            foreach (Ingredient.Attribute attribute in attributes)
            {
                switch (attribute)
                {
                    case Ingredient.Attribute.burn:
                        tooltip += string.Format("\n{0}% Burn", Mathf.RoundToInt(stats.burn * 100));
                        break;
                    case Ingredient.Attribute.splash:
                        tooltip += string.Format("\n{0}% Splash", Mathf.RoundToInt(stats.splash * 100));
                        break;
                    case Ingredient.Attribute.leech:
                        tooltip += string.Format("\n{0}% Lifesteal", Mathf.RoundToInt(stats.lifesteal * 100));
                        break;
                    case Ingredient.Attribute.atkup:
                        tooltip += string.Format("\n{0}% Attack Boost", Mathf.RoundToInt(stats.atkboost * 100));
                        break;
                    case Ingredient.Attribute.atkdown:
                        tooltip += string.Format("\n{0}% Attack Boost", Mathf.RoundToInt(stats.atkdebuff * 100));
                        break;
                    case Ingredient.Attribute.stun:
                        tooltip += string.Format("\n{0}% Attack Boost", Mathf.RoundToInt(stats.stun * 100));
                        break;
                }
            }
        }
        

        return tooltip;
    }
}
