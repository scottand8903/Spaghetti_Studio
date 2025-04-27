//Start() sets the SFX to play even when the game is paused.
//Update() listens every frame for Esc key input.
//Pressing ESC triggers TogglePause(), which plays/pauses the game and the SFX depending on pause state.

using UnityEngine;

public class PauseWithSFX : MonoBehaviour
{
    public AudioSource pauseSFX;  // <- Assign pause sound here in Inspector
    private bool isPaused = false;

    // Called at the beginning of the scene
    void Start()
    {
        if (pauseSFX != null)
        {
            pauseSFX.ignoreListenerPause = true; // <- Let SFX play even when game is paused
        }
    }

    // Checks every frame for user input
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); // <- Trigger pause sound logic
        }
    }

    void TogglePause()
    {
        if (!isPaused)
        {
            // Play pause sound effect
            if (pauseSFX != null && !pauseSFX.isPlaying)
            {
                pauseSFX.Play(); // <- Plays the SFX right when paused
            }

            Time.timeScale = 0f; // <- Game paused
        }
        else
        {
            // Stop SFX when resuming
            if (pauseSFX != null && pauseSFX.isPlaying)
            {
                pauseSFX.Stop(); // <- SFX stops when unpausing
            }

            Time.timeScale = 1f; // <- Game resumed
        }

        isPaused = !isPaused;
    }
}
