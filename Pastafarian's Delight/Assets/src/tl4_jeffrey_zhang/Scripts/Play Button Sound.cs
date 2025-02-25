using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public AudioSource audio;
    public void playButton()
    {
        audio.Play();
    }
}