//Start() is called when the game scene loads
//Update() runs every frame (60 fps with vsync by default)
//When player presses Escape, TogglePause() runs to pause and the music pauses.

using UnityEngine;

public class PlayBackgroundMusic : MonoBehaviour
{
    public AudioSource backgroundAudio;  // <- This is linked in Unity’s Inspector
    private bool isPaused = false;

    // Called automatically by Unity when the scene starts
    void Start()
    {
        if (backgroundAudio != null)
        {
            backgroundAudio.Play(); // <- Music starts playing right as the scene starts
        }
    }

    // Called every frame by Unity's engine
    void Update()
    {
        // Listen for Esc key every frame
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); // <- This is where pausing/unpausing logic kicks in
        }
    }

    void TogglePause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f; // <- Pause the game time

            // Pause background music
            if (backgroundAudio != null && backgroundAudio.isPlaying)
            {
                backgroundAudio.Pause(); // <- Music pauses here
            }
        }
        else
        {
            Time.timeScale = 1f; // <- Resume game time

            // Resume background music
            if (backgroundAudio != null && !backgroundAudio.isPlaying)
            {
                backgroundAudio.UnPause(); // <- Music resumes here
            }
        }

        isPaused = !isPaused; // <- Toggle pause state
    }
}
