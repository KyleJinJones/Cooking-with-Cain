using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ResultText : MonoBehaviour
{
    Text text;
    public static List<string> lines = new List<string>();
    static ResultText instance;

    public Slider slider;

    void Awake()
    {
        instance = this;

        text = GetComponent<Text>();
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
}
