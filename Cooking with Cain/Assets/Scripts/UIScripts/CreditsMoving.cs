using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMoving : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0f, 50f*Time.deltaTime, 0f));
    }
}
