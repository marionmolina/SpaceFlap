using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public Text gameOverScoreText; // Text for displaying score in the game over panel
    public Text bestScoreText; // Text for displaying the best score
    public GameObject playButton;
    public GameObject retryButton;
    public GameObject gameOver;
    public GameObject pausePanel;
    public AudioSource musicSource;  // Reference to the AudioSource for background music

    private int score;
    private bool isGameOver;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Play();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        isGameOver = false;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        Time.timeScale = 1f;
        player.enabled = true;

        // Start or resume the music when play is triggered
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }

        // Destroy all existing pipes
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        // Check and update the best score
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        // Update UI
        gameOverScoreText.text = "Your Score: " + score;
        bestScoreText.text = "Best Score: " + bestScore;

        gameOver.SetActive(true);
        playButton.SetActive(true);
        Pause();

        // Pause the music when game is over
        musicSource.Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
