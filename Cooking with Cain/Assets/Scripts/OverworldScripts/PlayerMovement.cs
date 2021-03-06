﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float speed;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
       // GameObject myGameObject = new GameObject("Test Object"); // Make a new GO.
       // Rigidbody gameObjectsRigidBody = myGameObject.AddComponent<Rigidbody>(); // Add the rigidbody.
        //gameObjectsRigidBody.mass = 5; // Set the GO's mass to 5 via the Rigidbody.
    }

    // Update is called once per frame
    // Muhammad 1/30/2019: Don't use FixedUpdate. It limits to 50 fps
    void Update()
    {
        GetInput();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }
}
