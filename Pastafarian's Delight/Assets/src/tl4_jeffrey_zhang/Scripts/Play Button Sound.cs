using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audio;
    public float Sounditerationspeed = 1.0f; // Added this missing variable

    public void PlayButton()
    {
        audio.pitch = Sounditerationspeed; // Adjusts pitch to simulate speed increase
        audio.Play();
    }
}

