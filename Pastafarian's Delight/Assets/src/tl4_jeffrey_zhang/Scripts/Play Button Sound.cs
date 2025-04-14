//Technically I should've named this file PlayAudio.cs, but I've created it much earlier and I don't want to rename it.
// This only runs when the player clicks a UI Button in the menu (Like "Start Game", or a button on the Pause menu)
//PlayButton() is wired to the button via Unity's OnClick() event system

using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audio; // <- Set this to your button click sound
    public float Sounditerationspeed = 1.0f; // <- Can tweak pitch for effect

    // This function is called through Unity's UI Button OnClick()
    public void PlayButton()
    {
        audio.pitch = Sounditerationspeed; // <- You can customize the pitch if needed
        audio.Play(); // <- This plays when the user clicks the UI button
    }
}
