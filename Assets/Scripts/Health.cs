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