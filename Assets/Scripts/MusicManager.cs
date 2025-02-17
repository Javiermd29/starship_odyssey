using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{

    private AudioSource audioSource;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip clickSound;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;

    private void Awake()
    {

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
}
