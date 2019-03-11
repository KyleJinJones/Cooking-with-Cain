using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ResultText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshProUGUI text;
    public static List<string> lines = new List<string>();

    public Slider slider;

    bool mouseOver = false;

    void Awake()
    {
        lines.Clear();
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
        text.color = Color.black;
        
        slider.gameObject.SetActive(false);
    }
    
    void Update()
    {
        string str = "";

        if (lines.Count > 10)
        {
            slider.gameObject.SetActive(true);
            if (mouseOver)
                slider.value += Input.GetAxisRaw("Mouse ScrollWheel") * 10;
            slider.maxValue = lines.Count - 10;

            for (int i = lines.Count - Mathf.RoundToInt(slider.value) - 10; i < lines.Count - Mathf.RoundToInt(slider.value); i++)
            {
                str += lines[i] + "\n";
            }
        }
        else
        {
            for (int i = 0; i < lines.Count; i++)
            {
                str += lines[i] + "\n";
            }
        }

        text.text = str;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
