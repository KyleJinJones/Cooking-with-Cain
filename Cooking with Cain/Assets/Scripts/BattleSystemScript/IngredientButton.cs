using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]
public class IngredientButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Ingredient ingredient;
    public IngredientSelector selector;
    public int buttonPosition;

    Entity player;

    bool selected;
    Image buttonImage;
    Image foodImage;
    Coroutine fade = null;

    Color lightGray = new Color(0.8f, 0.8f, 0.8f);
    Color gray = new Color(0.6f, 0.6f, 0.6f);
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
    }

    void Start()
    {
        ingredient = IngredientSelector.equipped[buttonPosition];
        buttonImage = GetComponent<Image>();

        GameObject obj = new GameObject("Icon");
        obj.transform.SetParent(transform, false);

        if (ingredient != null)
        {
            foodImage = obj.AddComponent<Image>();
        }
    }

    void Update()
    {
        if (ingredient != null)
        {
            foodImage.sprite = ingredient.sprite;
            foodImage.SetNativeSize();

            float attack = player.GetEffectiveAttack();

            string tooltip = ingredient.foodName;

            switch (ingredient.damageType)
            {
                case Ingredient.DamageType.flat:
                    tooltip += string.Format("\n+{0} damage", Mathf.RoundToInt(attack * ingredient.multiplier));
                    break;
                case Ingredient.DamageType.range:
                    tooltip += string.Format("\n+{0}~{1} damage", Mathf.RoundToInt(attack * ingredient.multiplierMin), Mathf.RoundToInt(attack * ingredient.multiplierMax));
                    break;
            }

            Stats stats = player.stats;

            switch (ingredient.attribute)
            {
                case Ingredient.Attribute.burn:
                    tooltip += string.Format("\nEffect: {0}% Burn", Mathf.RoundToInt(stats.burn * 100));
                    break;
                case Ingredient.Attribute.splash:
                    tooltip += string.Format("\nEffect: {0}% Splash", Mathf.RoundToInt(stats.splash * 100));
                    break;
                case Ingredient.Attribute.leech:
                    tooltip += string.Format("\nEffect: {0}% Lifesteal", Mathf.RoundToInt(stats.lifesteal * 100));
                    break;
                case Ingredient.Attribute.atkup:
                    tooltip += string.Format("\nEffect: {0}% Attack boost", Mathf.RoundToInt(stats.atkboost * 100));
                    break;
                case Ingredient.Attribute.atkdown:
                    tooltip += string.Format("\nEffect: {0}% Attack debuff", Mathf.RoundToInt(stats.atkdebuff * 100));
                    break;
                case Ingredient.Attribute.stun:
                    tooltip += string.Format("\nEffect: {0}% Stun chance", Mathf.RoundToInt(stats.stun * 100));
                    break;
                case Ingredient.Attribute.defup:
                    tooltip += string.Format("\nEffect: {0}% Defense boost", Mathf.RoundToInt(stats.defboost * 100));
                    break;
                case Ingredient.Attribute.defdown:
                    tooltip += string.Format("\nEffect: {0}% Defense debuff", Mathf.RoundToInt(stats.defdebuff * 100));
                    break;
                case Ingredient.Attribute.reflect:
                    tooltip += string.Format("\nEffect: {0}% Reflect", Mathf.RoundToInt(stats.reflect * 100));
                    break;
                case Ingredient.Attribute.cleanse:
                    tooltip += string.Format("\nEffect: Debuff cleanse");
                    break;
                case Ingredient.Attribute.selfdmg:
                    tooltip += string.Format("\nEffect: {0}% Self damage", Mathf.RoundToInt(stats.selfdmg * 100));
                    break;
                case Ingredient.Attribute.miss:
                    tooltip += string.Format("\nEffect: {0}% Miss chance", Mathf.RoundToInt(stats.miss * 100));
                    break;
            }

            TooltipText tooltipText = gameObject.GetComponent<TooltipText>();
            if (tooltipText == null)
                tooltipText = gameObject.AddComponent<TooltipText>();

            tooltipText.text = tooltip;
        }
    }

    public void Deselect()
    {
        selected = false;
        buttonImage.color = Color.white;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (fade != null)
            StopCoroutine(fade);

        fade = StartCoroutine(FadeColor(lightGray));
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (fade != null)
            StopCoroutine(fade);

        if (selected)
            fade = StartCoroutine(FadeColor(gray));
        else
            fade = StartCoroutine(FadeColor(Color.white));
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (ingredient != null)
            selected = selector.Select(this);
    }

    IEnumerator FadeColor(Color color)
    {
        Color current = buttonImage.color;

        for (int i = 0; i < 10; i++)
        {
            buttonImage.color = Color.Lerp(current, color, (i + 1) / 10f);
            yield return null;
        }
    }
}