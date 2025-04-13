using UnityEngine;

public class PlayBackgroundMusic : MonoBehaviour
{
    public AudioSource backgroundAudio;  // Assign this in Inspector
    private bool isPaused = false;

    void Start()
    {
        if (backgroundAudio != null)
        {
            backgroundAudio.Play();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;

            // Pause audio
            if (backgroundAudio != null && backgroundAudio.isPlaying)
            {
                backgroundAudio.Pause();
            }
        }
        else
        {
            Time.timeScale = 1f;

            // Resume audio
            if (backgroundAudio != null && !backgroundAudio.isPlaying)
            {
                backgroundAudio.UnPause();
            }
        }

        isPaused = !isPaused;
    }
}
