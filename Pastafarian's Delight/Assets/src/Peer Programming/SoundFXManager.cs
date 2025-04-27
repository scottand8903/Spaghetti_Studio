using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.Audio;

public class SoundFXManager : AudioManager
{
    // Singleton instance
    private static SoundFXManager _instance;
    private IAudioSourceFactory soundFXSourceFactory = new AudioSourceFactory();
    private GameObject player;
    private PlayerController playerMovement;
    // This allows other classes to access the instance of SoundFXManager
    // while ensuring that only one instance exists (singleton pattern).
    public static SoundFXManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SoundFXManager instance is null. Ensure SoundFXManager is in the scene.");
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }


    // Ensure only one instance exists
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeSoundFXSources();
    }
    void Update()
    {
        PlayFootSteps();
    }
    public void AddAudioSource(string key, string resourceName)
    {
        AudioSource audioSource = soundFXSourceFactory.CreateAudioSource(resourceName);
        audioSources[key] = audioSource;

        Debug.Log($"Audio source '{key}' added successfully.");
    }

    // Initialize sound effects
    private void InitializeSoundFXSources()
    {
        Debug.Log("Initializing soundFXSources...");

        AddAudioSource("Chomp","Sounds/chomp");
        AddAudioSource("Footsteps", "Sounds/Playermove");
        AddAudioSource("Playerhit", "Sounds/Playerhit");
        AddAudioSource("Heal", "Sounds/heal");
        // Add sound effects to the audioSources dictionary

        Debug.Log("soundFXSources initialized successfully.");
    }


    public System.Collections.IEnumerator PlaySoundWithDelay(string soundKey, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySound(soundKey);
    }

    public void PlaysoundwithLoop(string soundKey)
    {
        if (audioSources.TryGetValue(soundKey, out var audioSource))
        {
            audioSource.loop = true; // Set the loop property
            PlaySound(soundKey); // Play the sound
        }
        else
        {
            Debug.LogWarning($"Sound key '{soundKey}' not found in AudioManager!");
        }
    }

    public void PlayFootSteps(){
        bool pressingUp = Input.GetKey(KeyCode.W);
        bool pressingLeft = Input.GetKey(KeyCode.A);
        bool pressingDown = Input.GetKey(KeyCode.S);
        bool pressingRight = Input.GetKey(KeyCode.D);
        if(IsPlaying("Footsteps") == false){
            if (pressingUp || pressingLeft || pressingDown || pressingRight)
            {
                PlaySound("Footsteps");
            }
        }
        else if (pressingUp == false && pressingLeft == false && pressingDown == false && pressingRight == false)
        {
            StopSound("Footsteps");
        }
    }
}



