using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float movementRange = 25f; // Distance to move before turning
    private Vector3 startingPosition;
    private int direction = 1; // 1 for right, -1 for left

    void Start()
    {
        startingPosition = transform.position; // Record the starting position
    }

    void Update()
    {
        // Move left and right
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        // Check if the enemy has moved too far from the starting position
        if (Mathf.Abs(transform.position.x - startingPosition.x) >= movementRange)
        {
            direction *= -1; // Reverse direction
        }
    }

    // Optional: Reverse direction if hitting a wall or obstacle
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            direction *= -1; // Reverse direction when hitting an obstacle
        }
    }
}
