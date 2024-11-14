using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public string enemyTag = "Enemy"; // Tag for enemy or dangerous objects

    // This method is called when the player collides with another object
    void OnCollisionEnter(Collision collision)
    {
        // Check if the object has the enemy tag
        if (collision.gameObject.CompareTag(enemyTag))
        {
            Die();
        }
    }

    // This method is called when the player enters a trigger zone
    void OnTriggerEnter(Collider other)
    {
        // Check if the object has the enemy tag
        if (other.CompareTag(enemyTag))
        {
            Die();
        }
    }

    // Handle the player's death and restart the scene
    void Die()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
