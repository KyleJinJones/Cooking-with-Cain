using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public GameObject Cain;


    // When button is pressed, changes the attribute.
    public void ChangeAttribute(string attribute)
    {

        if (attribute == "health")
        {
            Cain.GetComponent<Health>().health += 10.0f;
        }
        else if (attribute == "splash")
        {
            Cain.GetComponent<AttributeStats>().splash *= 1.10f;
        }
        else if (attribute == "lifesteal")
        {
            Cain.GetComponent<AttributeStats>().lifesteal *= 1.10f;
        }
        else if (attribute == "atkboost")
        {
            Cain.GetComponent<AttributeStats>().atkboost += 3.0f;
        }


    }



}
