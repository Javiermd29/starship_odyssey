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

        if (Instance != null)
        {
            Debug.LogError("There's more than one instance");
        }
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        audioSource.volume = savedVolume;

        // Sincronizar el slider si existe
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

    }

    private void Start()
    {

        // Suscribir el slider al método que actualiza el volumen
        //volumeSlider.onValueChanged.AddListener(SetVolume);

        muteToggle.onValueChanged.AddListener(SetMusicMute);

        bool isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        audioSource.mute = isMuted;
        muteToggle.isOn = isMuted;

    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    public void SetMusicMute(bool isMuted)
    {
        audioSource.mute = isMuted;

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
