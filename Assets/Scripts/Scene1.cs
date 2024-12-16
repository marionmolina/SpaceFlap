using UnityEngine;

public class Scene1 : MonoBehaviour
{
    public AudioSource musicSource;  // This must be public to appear in the Inspector

    private const string MUSIC_PREF_KEY = "MusicMuted";

    private void Start()
    {
        // Load the music preference from PlayerPrefs
        bool isMuted = PlayerPrefs.GetInt(MUSIC_PREF_KEY, 0) == 1;
        
        // Apply the mute setting to the music source
        musicSource.mute = isMuted;

        // Optionally, start playing the music if not muted
        if (!isMuted)
        {
            musicSource.Play();
        }
    }
}
