using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseMenuUI; // Reference to the pause menu UI
    public Button resumeButton;    // Reference to the resume button
    public Button quitButton;      // Reference to the quit button

    private bool isPaused = false; // Tracks if the game is paused

    void Start()
    {
        // Ensure the pause menu is initially hidden
        pauseMenuUI.SetActive(false);

        // Assign button functionality
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        // Toggle pause menu with the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Show pause menu
        Time.timeScale = 0f;         // Freeze the game
        isPaused = true;             // Update pause state
        Cursor.lockState = CursorLockMode.None; // Unlock cursor for UI interaction
        Cursor.visible = true;       // Make the cursor visible
    }

    void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Hide pause menu
        Time.timeScale = 1f;          // Resume the game
        isPaused = false;             // Update pause state
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor back for gameplay
        Cursor.visible = false;       // Hide the cursor
    }

    void QuitGame()
    {
        Time.timeScale = 1f;          // Reset time scale to normal
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene
    }
}
