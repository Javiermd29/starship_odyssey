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
    }

    private void Start()
    {
        volumeSlider.value = 0.5f;

        muteToggle.onValueChanged.AddListener(SetMusicMute);

        bool isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        audioSource.mute = isMuted;
        muteToggle.isOn = isMuted;

    }

    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
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
