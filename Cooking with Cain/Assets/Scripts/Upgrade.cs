using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public string nextScene;


    // When button is pressed, changes the attribute.
    public void ChangeAttribute(string attribute)
    {
        
        float num = PlayerPrefs.GetFloat(attribute);
        Debug.Log("Changing Attribute");

        if (attribute == "health")
        {
            num += 10.0f;
            PlayerPrefs.SetFloat("health", num);
        }
        else if (attribute == "splash")
        {
            num += .10f;
            PlayerPrefs.SetFloat("splash", num);
        }
        else if (attribute == "lifesteal")
        {
            num += .10f;
            PlayerPrefs.SetFloat("lifesteal", num);
        }
        else if (attribute == "atkboost")
        {
            num += .1f;
            PlayerPrefs.SetFloat("atkboost", num);
        }
        Debug.Log("Change attribute done");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }



}
