using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public float pushStrength = 5.0f; // Speed at which the box moves
    private bool isPlayer2Colliding = false; // Track if Player2 is colliding
    private Rigidbody rb;

    private void Start()
    {
        // Ensure the box has a Rigidbody and set it to be kinematic initially
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Prevent movement unless pushing
    }

    private void Update()
    {
        if (isPlayer2Colliding)
        {
            // Check if Player2 is pressing the push button
            if (Input.GetKey(KeyCode.E)) // "E" is the push button
            {
                rb.isKinematic = false; // Enable movement
                CheckForPlayer2Input();
            }
            else
            {
                rb.isKinematic = true; // Make the object immovable
            }
        }
        else
        {
            rb.isKinematic = true; // Ensure it stays immovable when not interacting
        }
    }

    private void CheckForPlayer2Input()
    {
        // Check if Player2 is pressing the left or right movement keys
        bool isPushingRight = Input.GetKey(KeyCode.D); // Right movement key
        bool isPushingLeft = Input.GetKey(KeyCode.A);  // Left movement key

        // Only move the box if Player2 is actively pressing left or right
        if (isPushingRight || isPushingLeft)
        {
            MoveBox(isPushingRight ? 1 : -1); // 1 for right, -1 for left
        }
    }

    private void MoveBox(float direction)
    {
        // Move the box smoothly in the specified direction along the X-axis
        Vector3 movement = Vector3.right * direction * pushStrength * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            // Track when Player2 starts colliding
            isPlayer2Colliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            // Reset when Player2 stops colliding
            isPlayer2Colliding = false;
        }
    }
}
