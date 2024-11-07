using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal"); // Left (-1) or Right (+1)
        float moveVertical = Input.GetAxis("Vertical"); // Forward (+1) or Backward (-1)

        // Create movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        // Apply movement to the player
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    // Apply jump force
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    // Check if player is grounded (for jump limitation)
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = true;
        }
    }
}
