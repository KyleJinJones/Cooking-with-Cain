using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
   public GameObject window=null;
    //Used to close a window by setting it inactive
   public void Close()
    {
        if (window != null)
        {
            window.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
