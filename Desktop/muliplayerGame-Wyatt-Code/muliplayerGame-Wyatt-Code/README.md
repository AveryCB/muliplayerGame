# muliplayerGame

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{
    public int damage = 10;
    public float knockbackForce = 5f;

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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class coins : MonoBehaviour
{
    public static int score = 0; // static score variable
    public Text scoreText; // Reference to the Text component
    public Text winText; // Text to display when score is 5

    private void Start()
    {
        // Initialize score text
        scoreText.text = "Score: " + score;
        // Initialize win text and hide it
        winText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Coin is collected
            this.gameObject.SetActive(false);

            // Increment score
            score++;

            // Update score text
            scoreText.text = "Score: " + score;

            // Check if score is 5
            if (score == 5)
            {
                // Update win text
                winText.text = "You win!";
            }
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Text healthText; // Reference to the UI Text component

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        // For testing purposes, you can press H to take damage and J to heal
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Heal(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthText();
        Debug.Log("Player took damage, current health: " + currentHealth);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthText();
        Debug.Log("Player healed, current health: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player died.");
        Destroy(gameObject); // Destroy the player GameObject
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + currentHealth;
    }
}
