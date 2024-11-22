using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{
    public int damage = 10; public float knockbackForce = 5f;

    // This method will be called when another collider enters the trigger collider attached to the object where this script is attached
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has a Health component
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            // Deal damage to the player
            playerHealth.TakeDamage(damage);

            // Apply knockback force
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 knockbackDirection = other.transform.position - transform.position;
                knockbackDirection.y = 0; // Keep the knockback force horizontal
                knockbackDirection.Normalize();
                playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
        }
    }

}