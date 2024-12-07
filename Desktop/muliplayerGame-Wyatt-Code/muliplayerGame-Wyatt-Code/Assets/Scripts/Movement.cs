using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float pushForce = 5f;  // Force to push obstacles
    public float pushDistance = 1.5f; // Distance to check for obstacles in front of Player Two

    private Rigidbody rb;
    private bool isGrounded;

    public bool isPlayerOne; // Set this to true for player one, false for player two in the Inspector
    public bool canJump; // Set to true for the player who should be able to jump, false for the other

    public Transform playerModel; // Assign the model in the Inspector
    public Vector3 rotationOffset = new Vector3(0f, 90f, 0f); // Adjust Y to fix the offset

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (isPlayerOne)
        {
            // Player One movement using WASD
            float moveHorizontal = Input.GetAxis("Horizontal"); // A (-1) or D (+1)
            float moveVertical = Input.GetAxis("Vertical"); // W (+1) or S (-1)

            movement = new Vector3(moveHorizontal, 0f, moveVertical);

            // Player One can jump
            if (canJump && Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }
        else
        {
            // Player Two movement using Arrow keys
            float moveHorizontal = Input.GetAxis("Horizontal2"); // Left (-1) or Right (+1)
            float moveVertical = Input.GetAxis("Vertical2"); // Up (+1) or Down (-1)

            movement = new Vector3(moveHorizontal, 0f, moveVertical);

            // Player Two cannot jump, but can push obstacles
            if (Input.GetKey(KeyCode.LeftShift))  // Hold Left Shift to push
            {
                PushObstacles();
            }
        }

        // Move the player and rotate the model
        if (movement.magnitude > 0.1f)
        {
            transform.Translate(movement.normalized * moveSpeed * Time.deltaTime, Space.World);

            // Calculate rotation with offset
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            toRotation *= Quaternion.Euler(rotationOffset);

            // Smoothly rotate the model
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, toRotation, Time.deltaTime * 10f);
        }
    }

    void Jump()
    {
        // Apply force to the player model's Rigidbody (child)
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // Immediately set grounded to false after jumping
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player lands on the ground
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Only set isGrounded to false when leaving ground or obstacle
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = false;
        }
    }

    // Player Two can push obstacles while holding Left Shift
    void PushObstacles()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pushDistance))
        {
            // If the obstacle has a Rigidbody, apply a push force
            Rigidbody obstacleRb = hit.collider.GetComponent<Rigidbody>();
            if (obstacleRb != null)
            {
                // Apply force to the obstacle (using a smooth push)
                obstacleRb.AddForce(transform.forward * pushForce, ForceMode.Impulse);
            }
        }
    }
}
