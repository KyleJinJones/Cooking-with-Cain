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

    void Awake()
    {
        instance = this;

        text = GetComponent<Text>();
        text.text = "";
        text.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        string str = "";

        for (int i = Mathf.Max(0, lines.Count - 10); i < lines.Count; i++)
        {
            str += lines[i] + "\n";
        }

        text.text = str;
    }

    public static void FadeReset()
    {
        instance.StartCoroutine(instance.Fade());
    }

    IEnumerator Fade()
    {
        for(int i = 0; i < 15; i++)
        {
            text.color = new Color(0, 0, 0, 1 - i / 15f);
            yield return null;
        }

        lines.Clear();
        text.text = "";
        text.color = Color.black;
    }
}
