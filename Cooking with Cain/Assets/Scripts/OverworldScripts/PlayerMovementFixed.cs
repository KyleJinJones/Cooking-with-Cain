using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFixed : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector2 direction;
    public static Vector2 spawnPosition;
    public static Vector2 checkpointPosition;

    // For testing purposes. CT
    public bool dontMoveToSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!dontMoveToSpawn)
            transform.position = spawnPosition;
    }

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
