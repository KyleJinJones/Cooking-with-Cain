using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Image image = null;
    public TextMeshProUGUI text = null;
    public GameObject ingredients = null;

    void Start()
    {
        image.raycastTarget = false;
        text.raycastTarget = false;
    }

    void Update()
    {
        transform.SetAsLastSibling();

        TooltipText hovered = TooltipText.hovered;
        TooltipTextWithIngredients hovered2 = hovered as TooltipTextWithIngredients;

        image.gameObject.SetActive(hovered != null && hovered.text != null);
        text.gameObject.SetActive(hovered != null && hovered.text != null);

        if (hovered != null && hovered.text != null)
        {
            text.text = hovered.text;
            Canvas.ForceUpdateCanvases();

            if (hovered2 == null)
                image.rectTransform.sizeDelta = text.rectTransform.sizeDelta + new Vector2(10, 10);
            else
                image.rectTransform.sizeDelta = new Vector2(Mathf.Max(text.rectTransform.sizeDelta.x, Mathf.Min(hovered2.sprites.Count, 3) * 60), text.rectTransform.sizeDelta.y + Mathf.CeilToInt(hovered2.sprites.Count / 3.0f) * 60) + new Vector2(10, 10);

            float sizeX = image.rectTransform.sizeDelta.x / 2 * image.rectTransform.lossyScale.x;
            float sizeY = image.rectTransform.sizeDelta.y / 2 * image.rectTransform.lossyScale.y;

            float posX = Input.mousePosition.x + sizeX;
            float posY = Input.mousePosition.y + sizeY;

            float camWidth = Camera.main.pixelWidth;
            float camHeight = Camera.main.pixelHeight;
            
            transform.position = new Vector2(Mathf.Min(posX, camWidth - sizeX), Mathf.Min(posY, camHeight - sizeY));
            text.transform.localPosition = new Vector2(-image.rectTransform.sizeDelta.x, image.rectTransform.sizeDelta.y)/ 2 + new Vector2(5, -5);
            ingredients.transform.localPosition = text.transform.localPosition + new Vector3(0, -text.rectTransform.sizeDelta.y);

            if (hovered2 != null)
            {
                for (int i = 0; i < hovered2.sprites.Count; i++)
                {
                    if (ingredients.transform.childCount > i)
                    {
                        Image image = ingredients.transform.GetChild(i).GetComponent<Image>();
                        image.sprite = hovered2.sprites[i];
                    }
                    else
                    {
                        GameObject obj = new GameObject("Ingredient Icon");
                        Image image = obj.AddComponent<Image>();
                        image.sprite = hovered2.sprites[i];
                        image.rectTransform.sizeDelta = new Vector2(60, 60);
                        image.raycastTarget = false;

                        obj.transform.SetParent(ingredients.transform);
                        image.rectTransform.localScale = new Vector2(1, 1);
                        obj.transform.localPosition = new Vector3(30 + (i % 3) * 60, -30 - 60 * Mathf.FloorToInt(i / 3));
                    }
                }

                for (int i = ingredients.transform.childCount - 1; i >= hovered2.sprites.Count; i--)
                {
                    Destroy(ingredients.transform.GetChild(i).gameObject);
                }
            }
        }

        if (hovered2 == null)
        {
            foreach (Transform child in ingredients.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
