using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    Image image = null;
    Text text = null;

    private void Start()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();

        image.raycastTarget = false;
        text.raycastTarget = false;
    }

    void Update()
    {
        TooltipText hovered = TooltipText.hovered;

        image.gameObject.SetActive(hovered != null);
        text.gameObject.SetActive(hovered != null);

        if (hovered != null)
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
