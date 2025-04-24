using UnityEngine;
using UnityEngine.Audio;

public interface IAudioSourceFactory
{
    AudioSource CreateAudioSource(string resourceName, AudioMixerGroup audioGroup = null);
}

public class AudioSourceFactory : IAudioSourceFactory
{
    public AudioSource CreateAudioSource(string resourceName, AudioMixerGroup audioGroup = null)
    {
        GameObject gameObject = new GameObject(resourceName);
        AudioClip clip = Resources.Load<AudioClip>(resourceName);
        if (clip == null)
        {
            Debug.LogError($"Audio clip '{resourceName}' could not be loaded! Ensure it is in the Resources folder.");
            return null;
        }

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        if(audioGroup != null)
        {
            audioSource.outputAudioMixerGroup = audioGroup; // Set the AudioMixerGroup
        }
        audioSource.playOnAwake = false; // Set to false to prevent auto-playing
        return audioSource;
    }
}