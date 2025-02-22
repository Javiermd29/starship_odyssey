using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource audioSource;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip clickSound;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;

    private void Awake()
    {
        // Check if an instance already exists to prevent duplicates
        if (Instance != null)
        {
            Debug.LogError("There's more than one instance");
        }
        Instance = this; // Set this object as the singleton instance

        audioSource = GetComponent<AudioSource>();

        // Retrieve the saved music volume from PlayerPrefs, defaulting to 1 (full volume) if not set
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        audioSource.volume = savedVolume;

        // Synchronize the volume slider if it exists
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

    }

    private void Start()
    {
        muteToggle.onValueChanged.AddListener(SetMusicMute); // Add a listener to the mute toggle to call SetMusicMute when its value changes

        bool isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1; // Retrieve the saved mute state from PlayerPrefs (default is 0, meaning not muted)
        audioSource.mute = isMuted; // Apply the saved mute state to the audio source
        muteToggle.isOn = isMuted; // Synchronize the mute toggle button with the saved state

    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;  // Set the audio source volume to the given value

        // Save the volume setting in PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    public void SetMusicMute(bool isMuted)
    {
        audioSource.mute = isMuted; // Mute or unmute the audio source based on the given value

        // Save the mute state in PlayerPrefs (1 for muted, 0 for unmuted)
        PlayerPrefs.SetInt("MusicMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();

    }

    public void PlaySFX(AudioClip clip)
    {

        if(clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }

    }
    public IEnumerator StartFade(float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        yield break;
    }

    internal static IEnumerator StartFade(object audioSource, int v1, int v2)
    {
        throw new NotImplementedException();
    }

}
