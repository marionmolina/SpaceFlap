using UnityEngine;
using UnityEngine.SceneManagement; // For loading scenes
using UnityEngine.UI; // For UI elements

public class PauseMenu : MonoBehaviour
{
    public GameManager gameManager;  // Reference to the GameManager
    public GameObject pausePanel;    // The pause panel UI element
    public Button resumeButton;      // Button to resume the game       
    public Button exitButton;        // Button to exit to MainMenu
    public Button pauseButton;       // Button to open the pause menu
    public string mainMenuSceneName = "MainMenu";  // The name of the Main Menu scene
    public AudioSource musicSource;  // Reference to the AudioSource for background music

    private bool isPaused = false;   // Track if the game is paused

    void Start()
    {
        // Ensure the pause panel is hidden at the start
        pausePanel.SetActive(false);

        // Assign button listeners
        resumeButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(ExitToMainMenu);
        pauseButton.onClick.AddListener(PauseGame);
    }

    void Update()
    {
        // Toggle pause when the "Escape" key is pressed (optional)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        // Update the Resume button's interactability based on game-over status
        resumeButton.interactable = !gameManager.IsGameOver();
    }

    public void PauseGame()
    {
        // Show the pause panel and pause the game
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game time
        isPaused = true;

        // Stop the music when the game is paused
        musicSource.Pause();
    }

    public void ResumeGame()
    {
        // Hide the pause panel and resume the game
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game time
        isPaused = false;

        // Resume the music when the game is resumed
        musicSource.Play();
    }

    public void ExitToMainMenu()
    {
        // Resume game time before transitioning to the main menu
        Time.timeScale = 1f;
        
        // Stop music when exiting to the main menu
        musicSource.Stop();

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
