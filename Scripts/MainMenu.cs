using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// This is the main menu lol
public class MainMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public Button newGameButton; // Reference to the New Game button
    public Button quitButton;    // Reference to the Quit button

    void Start()
    {
        // Assign button functionality when the game starts
        if (newGameButton != null)
            newGameButton.onClick.AddListener(NewGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    // Method to load the scene
    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");  
    }

    // Method to quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // Stop playing in the editor
        #else
            Application.Quit();  // Quit the game in a built version
        #endif
    }
}
