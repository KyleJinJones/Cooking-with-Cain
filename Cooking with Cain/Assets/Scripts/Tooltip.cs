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
    }

    void Update()
    {
        TooltipText hovered = TooltipText.hovered;

        image.gameObject.SetActive(hovered != null);
        text.gameObject.SetActive(hovered != null);

        if (hovered != null)
        {
            text.text = hovered.GetText();
            Canvas.ForceUpdateCanvases();
            image.rectTransform.sizeDelta = text.rectTransform.sizeDelta + new Vector2(5, 5);
            float sizeX = image.rectTransform.sizeDelta.x / 2;
            float sizeY = image.rectTransform.sizeDelta.y / 2;

            float posX = Input.mousePosition.x + sizeX;
            float posY = Input.mousePosition.y + sizeY;

            float camWidth = Camera.current.pixelWidth;
            float camHeight = Camera.current.pixelHeight;

            transform.position = new Vector3(Mathf.Min(posX, camWidth - sizeX), Mathf.Min(posY, camHeight - sizeY));
        }
    }
}
