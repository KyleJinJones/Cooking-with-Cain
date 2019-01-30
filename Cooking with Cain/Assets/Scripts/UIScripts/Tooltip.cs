using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    Image image = null;
    TextMeshProUGUI text = null;

    void Awake()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();

        image.raycastTarget = false;
        text.raycastTarget = false;
    }

    void Update()
    {
        transform.SetAsLastSibling();

        TooltipText hovered = TooltipText.hovered;

        image.gameObject.SetActive(hovered != null && hovered.text != null);
        text.gameObject.SetActive(hovered != null && hovered.text != null);

        if (hovered != null && hovered.text != null)
        {
            text.text = hovered.text;
            Canvas.ForceUpdateCanvases();
            image.rectTransform.sizeDelta = text.rectTransform.sizeDelta + new Vector2(10, 10);
            float sizeX = image.rectTransform.sizeDelta.x / 2 * image.rectTransform.lossyScale.x;
            float sizeY = image.rectTransform.sizeDelta.y / 2 * image.rectTransform.lossyScale.y;

            float posX = Input.mousePosition.x + sizeX;
            float posY = Input.mousePosition.y + sizeY;

            float camWidth = Camera.main.pixelWidth;
            float camHeight = Camera.main.pixelHeight;
            
            transform.position = new Vector3(Mathf.Min(posX, camWidth - sizeX), Mathf.Min(posY, camHeight - sizeY));
        }
    }
}
