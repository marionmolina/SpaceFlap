using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject optionsPanel; // Drag your Options Panel here
    public Toggle musicToggle;      // Drag your single Toggle here

    [Header("Audio")]
    public AudioSource musicSource; // Assign your BackgroundMusic AudioSource here

    private const string MUSIC_PREF_KEY = "MusicMuted";

    private void Start()
    {
        // Load saved music preference
        bool isMuted = PlayerPrefs.GetInt(MUSIC_PREF_KEY, 0) == 1;
        musicSource.mute = isMuted;

        // Sync toggle with saved preference
        musicToggle.isOn = !isMuted;

        // Add listener for toggle
        musicToggle.onValueChanged.AddListener(SetMusicPreference);

        // Start playing the music only if not muted
        if (!isMuted && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    // Save and apply music preference
    public void SetMusicPreference(bool isOn)
    {
        bool isMuted = !isOn; // Unchecked = muted
        musicSource.mute = isMuted;

        PlayerPrefs.SetInt(MUSIC_PREF_KEY, isMuted ? 1 : 0); // Save muted (1) or unmuted (0)
        PlayerPrefs.Save();
    }

    // Method for starting the game
    public void StartButton()
    {
        SceneManager.LoadScene("Scene1"); // Replace "Scene1" with your game scene name
    }

    // Method for quitting the game
    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Game is exiting"); // Only works in a built game
    }

    // Show the Options Panel
    public void OptionButton()
    {
        optionsPanel.SetActive(true);
    }

    // Close the Options Panel
    public void CloseOptionsButton()
    {
        optionsPanel.SetActive(false);
    }
}
