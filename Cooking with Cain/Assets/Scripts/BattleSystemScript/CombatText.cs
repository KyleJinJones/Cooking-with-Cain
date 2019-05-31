using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatText : MonoBehaviour
{
    // Start is called before the first frame update
    static CombatText instance = null;
    public GameObject txtbox;
    public TextMeshProUGUI txt;

    void Start()
    {
        instance = this;
    }

    public IEnumerator DisplayLine(string str)
    {
        txtbox.SetActive(true);
        txt.text = str;
        float i = 0;
        while (i < .5f)
        { 
            yield return null;
             i += Time.deltaTime;
        }
        txtbox.SetActive(false);

    }
}
