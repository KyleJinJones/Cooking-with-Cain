using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddIng : MonoBehaviour
{

    public IngredientManager im;
    public int ingindex;
    public void adding()
    {
        for(int i=1; i < 13; i++)
        {
            if (PlayerPrefs.GetInt("Spare" + i) == -1)
            {
                PlayerPrefs.SetInt("Spare" + i, ingindex);
                break;
            }
        }
    }
}
