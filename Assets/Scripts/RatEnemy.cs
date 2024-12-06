using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemy : MonoBehaviour
{
    public Transform patrolPoint1;
    public Transform patrolPoint2;
    public float moveSpeed = 3f;
    public float chaseRange = 10f;
    public float fleeRange = 10f;
    public float avoidRange = 5f;

    private Transform currentPatrolPoint;
    private GameObject player1; // Rat
    private GameObject player2; // Raccoon
    private bool isChasing = false;
    private bool isFleeing = false;

    void Start()
    {
        currentPatrolPoint = patrolPoint1;
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
    }

    void Update()
    {
        float distanceToPlayer1 = Vector3.Distance(transform.position, player1.transform.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, player2.transform.position);

        if (distanceToPlayer1 <= chaseRange && distanceToPlayer2 <= fleeRange)
        {
            // Both players are within range, run away
            RunAway();
        }
        else if (distanceToPlayer2 <= fleeRange)
        {
            // Flee from Player 2 (Raccoon)
            Flee(player2.transform);
        }
        else if (distanceToPlayer1 <= chaseRange)
        {
            // Chase Player 1 (Rat)
            Chase(player1.transform);
        }
        else
        {
            // Patrol between points
            Patrol();
        }
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) < 0.1f)
        {
            currentPatrolPoint = currentPatrolPoint == patrolPoint1 ? patrolPoint2 : patrolPoint1;
        }

        MoveTowards(currentPatrolPoint.position);
    }

    void Chase(Transform target)
    {
        isChasing = true;
        isFleeing = false;
        MoveTowards(target.position);
    }

    void Flee(Transform target)
    {
        isChasing = false;
        isFleeing = true;
        Vector3 directionAway = (transform.position - target.position).normalized;
        Vector3 fleePosition = transform.position + directionAway * moveSpeed * Time.deltaTime;
        MoveTowards(fleePosition);
    }

    void RunAway()
    {
        isChasing = false;
        isFleeing = true;
        Vector3 directionAway = (transform.position - player2.transform.position).normalized;
        Vector3 runAwayPosition = transform.position + directionAway * moveSpeed * Time.deltaTime;
        MoveTowards(runAwayPosition);
    }

    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            // Rat (Player 1) is touched by the enemy, they die
            Destroy(other.gameObject);
            // Call GameManager to handle player death
            GameManager.instance.HandlePlayerDeath();
        }
    }
}