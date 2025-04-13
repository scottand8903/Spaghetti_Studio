using UnityEngine;

public class PauseWithSFX : MonoBehaviour
{
    public AudioSource pauseSFX;  // Assign in Inspector
    private bool isPaused = false;

    void Start()
    {
        if (pauseSFX != null)
        {
            pauseSFX.ignoreListenerPause = true;
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
            // Play pause SFX
            if (pauseSFX != null && !pauseSFX.isPlaying)
            {
                pauseSFX.Play();
            }

            Time.timeScale = 0f;
        }
        else
        {
            // Stop pause SFX
            if (pauseSFX != null && pauseSFX.isPlaying)
            {
                pauseSFX.Stop();
            }

            Time.timeScale = 1f;
        }

        isPaused = !isPaused;
    }
}
