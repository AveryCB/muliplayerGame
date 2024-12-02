using System.Collections;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the car
    public Vector3 direction = Vector3.forward; // Direction the car should drive

    void Update()
    {
        // Move the car in the assigned direction
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            // Player is touched by the car, they die
            Destroy(other.gameObject);
            // Call GameManager to handle player death
            GameManager.instance.HandlePlayerDeath();
        }
    }
}