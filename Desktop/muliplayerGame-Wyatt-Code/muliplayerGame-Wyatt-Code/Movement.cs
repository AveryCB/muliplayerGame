using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private bool isGrounded;

    public bool isPlayerOne; // Set this to true for player one, false for player two in the Inspector
    public bool canJump; // Set to true for the player who should be able to jump, false for the other

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isPlayerOne)
        {
            // Player One movement using WASD
            float moveHorizontal = Input.GetAxis("Horizontal"); // A (-1) or D (+1)
            float moveVertical = Input.GetAxis("Vertical"); // W (+1) or S (-1)

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            transform.Translate(movement * moveSpeed * Time.deltaTime);

            // Check for jump if this player is allowed to jump
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

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            transform.Translate(movement * moveSpeed * Time.deltaTime);

            // No jump functionality for Player Two
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = true;
        }
    }
}
