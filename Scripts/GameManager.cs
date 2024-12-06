using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text loseText; // Reference to the Text component for "You Lose" message
    public Text winText; // Reference to the Text component for "You Win" message

    private void Awake()
    {
        // Ensure there is only one instance of GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HandlePlayerDeath()
    {
        StartCoroutine(ShowLoseTextAndSwitchScene());
    }

    public void HandlePlayerWin()
    {
        StartCoroutine(ShowWinTextAndSwitchScene());
    }

    private IEnumerator ShowLoseTextAndSwitchScene()
    {
        // Ensure win text is hidden
        if (winText != null)
        {
            winText.gameObject.SetActive(false);
        }

        // Display "You Lose" text
        if (loseText != null)
        {
            loseText.text = "You Lose!";
            loseText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("loseText is not assigned in the GameManager.");
        }

        // Pause the game
        Time.timeScale = 0f;

        // Wait for 2 seconds in real time
        yield return new WaitForSecondsRealtime(2f);

        // Switch to MainMenu scene
        SceneManager.LoadScene("MainMenu");

        // Resume the game (in case the MainMenu scene is not loaded immediately)
        Time.timeScale = 1f;
    }

    private IEnumerator ShowWinTextAndSwitchScene()
    {
        // Ensure lose text is hidden
        if (loseText != null)
        {
            loseText.gameObject.SetActive(false);
        }

        // Display "You Win" text
        if (winText != null)
        {
            winText.text = "You Win!";
            winText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("winText is not assigned in the GameManager.");
        }

        // Pause the game
        Time.timeScale = 0f;

        // Wait for 2 seconds in real time
        yield return new WaitForSecondsRealtime(2f);

        // Switch to MainMenu scene
        SceneManager.LoadScene("MainMenu");

        // Resume the game (in case the MainMenu scene is not loaded immediately)
        Time.timeScale = 1f;
    }
}