using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// General AI movement for enemies that one shot both players
public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float movementRange = 25f; // Distance to move before turning
    public float chaseRange = 10f; // Range from players to start chasing
    private Vector3 startingPosition;
    private int direction = 1; // 1 for right, -1 for left
    private List<GameObject> playersAndRats;

    void Start()
    {
        startingPosition = transform.position; // Record the starting position

        // Find all players and rats
        playersAndRats = new List<GameObject>();
        playersAndRats.AddRange(GameObject.FindGameObjectsWithTag("Player1"));
        playersAndRats.AddRange(GameObject.FindGameObjectsWithTag("Player2"));
    }

    void Update()
    {
        GameObject target = GetTarget();
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= chaseRange)
        {
            // Chase the target
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            transform.Translate(directionToTarget * moveSpeed * Time.deltaTime);
        }
        else
        {
            // Move left and right
            transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

            // Check if the enemy has moved too far from the starting position
            if (Mathf.Abs(transform.position.x - startingPosition.x) >= movementRange)
            {
                direction *= -1; // Reverse direction
            }
        }
    }

    GameObject GetTarget()
    {
        GameObject nearestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject target in playersAndRats)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTarget = target;
            }
        }

        return nearestTarget;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            // Player is touched by the enemy, they die
            Destroy(other.gameObject);
            // Call GameManager to handle player death
            GameManager.instance.HandlePlayerDeath();
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