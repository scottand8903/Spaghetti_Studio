using UnityEngine;
using System.Collections.Generic;

public abstract class AudioManager : MonoBehaviour
{
    // Dictionary to store audio sources
    protected Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

    // Add an audio source using the factory
    // Play a sound by key
    //public void PlaySound(string soundKey)
    public virtual void PlaySound(string soundKey)
    {
        if (audioSources.TryGetValue(soundKey, out var audioSource))
        {
            if (audioSource != null)
            {
                if (audioSource.clip == null)
                {
                    Debug.LogError($"Audio clip for key '{soundKey}' is not assigned!");
                    return;
                }

                Debug.Log($"Playing sound for key '{soundKey}' with volume {audioSource.volume}");
                audioSource.Play();
            }
            else
            {
                Debug.LogError($"AudioSource for key '{soundKey}' is null!");
            }
        }
        else
        {
            Debug.LogWarning($"Sound key '{soundKey}' not found in AudioManager!");
        }
    }

    // Stop a sound by key
    public virtual void StopSound(string soundKey)
    {
        if (audioSources.TryGetValue(soundKey, out var audioSource))
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }
            else
            {
                Debug.LogError($"AudioSource for key '{soundKey}' is null!");
            }
        }
        else
        {
            Debug.LogWarning($"Sound key '{soundKey}' not found in AudioManager!");
        }
    }

    // Set volume for all audio sources
    public virtual void SetVolume(float volume)
    {
        foreach (var audioSource in audioSources.Values)
        {
            if (audioSource != null)
            {
                audioSource.volume = volume;
            }
            else
            {
                Debug.LogError("AudioSource is null!");
            }
        }
    }

    // Get the volume of a specific audio source
    public virtual float GetVolume(string soundKey)
    {
        if (audioSources.TryGetValue(soundKey, out var audioSource))
        {
            if (audioSource != null)
            {
                return audioSource.volume;
            }
            else
            {
                Debug.LogError($"AudioSource for key '{soundKey}' is null!");
            }
        }
        else
        {
            Debug.LogWarning($"Sound key '{soundKey}' not found in AudioManager!");
        }
        return 0f; // Default value if not found
    }

    // Set pitch for all audio sources
    public virtual void SetPitch(float pitch)
    {
        foreach (var audioSource in audioSources.Values)
        {
            if (audioSource != null)
            {
                audioSource.pitch = pitch;
            }
            else
            {
                Debug.LogError("AudioSource is null!");
            }
        }
    }

    // Get the pitch of a specific audio source
    public virtual float GetPitch(string soundKey)
    {
        if (audioSources.TryGetValue(soundKey, out var audioSource))
        {
            if (audioSource != null)
            {
                return audioSource.pitch;
            }
            else
            {
                Debug.LogError($"AudioSource for key '{soundKey}' is null!");
            }
        }
        else
        {
            Debug.LogWarning($"Sound key '{soundKey}' not found in AudioManager!");
        }
        return 0f; // Default value if not found
    }

    public bool IsPlaying(string soundKey)
    {
        if (audioSources.TryGetValue(soundKey, out var audioSource))
        {
            return audioSource.isPlaying;
        }
        else
        {
            Debug.LogWarning($"Sound key '{soundKey}' not found in AudioManager!");
            return false;
        }
    }

}