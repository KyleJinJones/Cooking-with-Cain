using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static TooltipText hovered = null;
    
    [Multiline]
    public string text = "";

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        hovered = this;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        hovered = null;
    }
}

public class TooltipTextWithIngredients : TooltipText
{
    public List<Sprite> sprites = new List<Sprite>();
}