using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peaMovement : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public float detectRadius;
    public int type; // Type 1 is Rectangle Patrol, 2 is Stationary Chase, 3 is Rectangular Chase
                    // Type 4 is Up/Down Patrol; 5 is Up/Down Chase
                    // Type 6 is Left/Right Patrol; 7 is Left/Right Chase
    
    public float patrolLength;
    
    private Rigidbody2D RB;
    private Collision2D collision;

    private Vector2 spawn;
    private Vector2 playerLocation;
    private Vector2 currentLocation;
    private Vector2 [] patrolArea = new Vector2[4];
    
    
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        spawn = transform.position;
        
        if (type == 4 | type == 5) {
            patrolArea[0].x = spawn.x;
            patrolArea[1].x = spawn.x;
            patrolArea[2].x = spawn.x;
            patrolArea[3].x = spawn.x;
            
            
            patrolArea[0].y = spawn.y + patrolLength;
            patrolArea[1].y = spawn.y - patrolLength;
            patrolArea[2].y = spawn.y + patrolLength;
            patrolArea[3].y = spawn.y - patrolLength;
            
        } else if (type == 6 | type == 7) {
            patrolArea[0].y = spawn.y;
            patrolArea[1].y = spawn.y;
            patrolArea[2].y = spawn.y;
            patrolArea[3].y = spawn.y;
            
            patrolArea[0].x = spawn.x + patrolLength;
            patrolArea[1].x = spawn.x - patrolLength;
            patrolArea[2].x = spawn.x + patrolLength;
            patrolArea[3].x = spawn.x - patrolLength;
            
        } else {
            patrolArea[0].x = spawn.x + patrolLength;
            patrolArea[0].y = spawn.y + patrolLength;
            patrolArea[1].x = spawn.x - patrolLength;
            patrolArea[1].y = spawn.y + patrolLength;
            patrolArea[2].x = spawn.x - patrolLength;
            patrolArea[2].y = spawn.y - patrolLength;
            patrolArea[3].x = spawn.x + patrolLength;
            patrolArea[3].y = spawn.y - patrolLength;
        }
    }

    void Update() {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
        currentLocation = transform.position;

        float distance = Vector2.Distance(playerLocation, spawn);
        
        if (type == 2) {
            if (distance <= detectRadius) {
                currentLocation = Vector2.MoveTowards(currentLocation, playerLocation, speed * Time.deltaTime);

            } else if (currentLocation != spawn) {
                currentLocation = Vector2.MoveTowards(currentLocation, spawn, speed * Time.deltaTime);
            }
        
        } else {
            if ((type == 3 | type == 5 | type == 7) && (distance <= detectRadius)) {
                currentLocation = Vector2.MoveTowards(currentLocation, playerLocation, speed * Time.deltaTime); 
            } else if (currentLocation == patrolArea[0]) {
                Vector2 temp = patrolArea[0];
                    
                patrolArea[0].Set(patrolArea[1].x, patrolArea[1].y);
                patrolArea[1].Set(patrolArea[2].x, patrolArea[2].y);
                patrolArea[2].Set(patrolArea[3].x, patrolArea[3].y);
                patrolArea[3].Set(temp.x, temp.y);
                    
            } else {
                currentLocation = Vector2.MoveTowards(currentLocation, patrolArea[0],speed * Time.deltaTime);
            }
        }
        
        transform.position = currentLocation;
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Make something happen here. - peaMovement.cs");
        }
    }
    
}
