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
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
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
                // Call GameManager to handle player win
                GameManager.instance.HandlePlayerWin();
            }
        }
    }
}