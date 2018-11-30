using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Upgrade : MonoBehaviour
{



    // When button is pressed, changes the attribute.
    public void ChangeAttribute(string attribute)
    {

        float num = PlayerPrefs.GetFloat(attribute);
        Debug.Log("Changing Attribute");

        if (attribute == "health")
        {
            num += 20.0f;
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
        else if (attribute == "atk")
        {
            num += 2.0f;
            PlayerPrefs.SetFloat("atk", num);
        }
        else if (attribute == "atkboost")
        {
            num += .1f;
            PlayerPrefs.SetFloat("atkboost", num);
        }
        else if (attribute == "stun")
        {
            num += .1f;
            PlayerPrefs.SetFloat("stun", num);
        }
        Debug.Log("Change attribute done");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level")+1);
    }



}
