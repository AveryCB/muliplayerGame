using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public float pushStrength = 5.0f; // Speed at which the box moves
    private bool isPlayerColliding = false;

    private void Update()
    {
        // Only allow movement if the player is colliding with the box
        if (isPlayerColliding)
        {
            CheckForPlayerInput();
        }
    }

    private void CheckForPlayerInput()
    {
        // Check if the player is pressing the left or right movement keys
        bool isPushingRight = Input.GetKey(KeyCode.D); // Right movement key
        bool isPushingLeft = Input.GetKey(KeyCode.A);  // Left movement key

        // Only move the box if the player is actively pressing left or right and colliding from the sides
        if (isPushingRight || isPushingLeft)
        {
            MoveBox(isPushingRight ? 1 : -1); // 1 for right, -1 for left
        }
    }

    private void MoveBox(float direction)
    {
        // Move the box smoothly in the specified direction along the X-axis
        transform.Translate(Vector3.right * direction * pushStrength * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        // Detect if the player is colliding with the box
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player's and box's positions
            Vector3 playerPosition = collision.transform.position;
            Vector3 boxPosition = transform.position;

            // Only allow pushing if the player is to the left or right of the box (not on top)
            if (Mathf.Abs(playerPosition.y - boxPosition.y) < 0.5f) // Adjust 0.5f for height tolerance
            {
                isPlayerColliding = true;
            }
            else
            {
                isPlayerColliding = false; // Disable moving if player is on top
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // If the player leaves the collision, stop the box from being pushed
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerColliding = false;
        }
    }
}
