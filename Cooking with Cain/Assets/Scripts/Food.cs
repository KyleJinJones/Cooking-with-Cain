using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    public float multiplier;
    public string foodName;
    public string attribute;

    // for ingredients such as ominous chicken with a variable multiplier. if it isnt defined, multiplier will be used instead
    public float[] multiplierRange;
}
