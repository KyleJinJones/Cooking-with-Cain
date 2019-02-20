using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFixed : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private static Vector2 direction;

    public static Vector2 spawnPosition;
    public static Vector2 checkpointPosition;

    public Sprite[] movingFrames;
    public Sprite[] idleFrames;

    // For testing purposes. CT
    public bool dontMoveToSpawn = false;
    private static bool spawned = false;

    private int frames = 0;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);

        if (!dontMoveToSpawn || spawned)
            transform.position = spawnPosition;

        spawned = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        GetInput();

        if (direction.sqrMagnitude > 0)
        {
            spriteRenderer.sprite = movingFrames[Mathf.FloorToInt(frames / 5f) % movingFrames.Length];
            transform.position += (Vector3) direction * speed * Time.deltaTime;
            transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);
        }
        else
        {
            spriteRenderer.sprite = idleFrames[Mathf.FloorToInt(frames / 5f) % idleFrames.Length];
        }

        frames++;
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

        direction.Normalize();
    }
}
