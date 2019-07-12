using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InbattlePotion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] HPpotion.PotionType potionType;
    [SerializeField] Image buttonImage;
    [SerializeField] EntityManager manager;

    TextMeshProUGUI text;
    Coroutine fade = null;

    Color darkGray = new Color(0.3f, 0.3f, 0.3f, 0.5f);
    Color gray = new Color(0.55f, 0.55f, 0.55f, 0.5f);

    int amount
    {
        get
        {
            return SaveDataManager.currentData.potions[(int)potionType];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        TooltipText tooltipText = gameObject.AddComponent<TooltipText>();
        tooltipText.text = string.Format("Heals {0}% HP", new int[] { 25, 50, 100 }[(int)potionType]);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = amount.ToString();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (fade != null && amount > 0)
            StopCoroutine(fade);

        fade = StartCoroutine(FadeColor(darkGray));
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (fade != null)
            StopCoroutine(fade);

        fade = StartCoroutine(FadeColor(gray));
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        manager.PlayerAction(potionType);
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
