using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public float pushStrength = 5.0f; // Strength of the box movement when pushing
    public float pushStrengthWhileShifting = 50.0f; // Increased strength while Player Two is pushing
    private Rigidbody rb;

    // Default mass for the box (higher to make it harder to move)
    public float defaultMass = 10f;
    // Lower mass when pushing (set to 1 for ease of moving)
    public float pushingMass = 1f;

    private bool isPlayerTwoPushing = false;  // Track if Player Two is pushing the box

    private void Start()
    {
        // Ensure the box has a Rigidbody
        rb = GetComponent<Rigidbody>();

        // Set Rigidbody to Kinematic to avoid moving from collisions by default
        rb.isKinematic = false;

        // Lock the rotation to prevent the box from spinning
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Set the initial mass of the box
        rb.mass = defaultMass;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Only react if Player Two collides with the box
        if (collision.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Player Two collided with the box");
            isPlayerTwoPushing = true;  // Player Two is now in contact
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Ensure Player Two is holding LeftShift while colliding with the box to continue pushing
        if (collision.gameObject.CompareTag("Player2") && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Player Two is pushing the box");

            // Temporarily decrease the Rigidbody's mass for easier pushing
            if (isPlayerTwoPushing)
            {
                rb.mass = pushingMass;  // Only change the mass if Player Two is pushing
                PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    // Apply a continuous force in the direction Player Two is facing
                    rb.AddForce(playerMovement.transform.forward * pushStrengthWhileShifting, ForceMode.Force);
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // When Player Two stops pushing or exits, return the box's mass to normal
        if (collision.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Player Two stopped pushing the box");
            isPlayerTwoPushing = false;  // Player Two is no longer in contact

            // Reset the mass of the box to its default value
            rb.mass = defaultMass;
        }
    }

    private void Update()
    {
        // In Update(), only ensure mass is adjusted while Player Two is holding LeftShift
        if (isPlayerTwoPushing && Input.GetKey(KeyCode.LeftShift))
        {
            // Temporarily reduce mass while pushing (only for Player Two)
            rb.mass = pushingMass;
        }
        else
        {
            // Reset mass when not pushing or Player Two is not holding LeftShift
            rb.mass = defaultMass;
        }
    }
}
