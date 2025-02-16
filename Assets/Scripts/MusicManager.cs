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

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        volumeSlider.value = 0.5f;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }

    public void PlaySFX(AudioClip clip)
    {

        if(clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }

    }

    /*public void PlaySoundAndLoadScene(int sceneNumber)
    {

        if (sfxSource != null && clickSound != null)
        {
            sfxSource.PlayOneShot(clickSound);
            StartCoroutine(LoadSceneAfterSound(sceneNumber));
        }
        else
        {
            SceneManager.LoadScene(1);
        }

    }

    private IEnumerator LoadSceneAfterSound(int sceneNumber)
    {
        yield return new WaitForSeconds(clickSound.length);
        SceneManager.LoadScene(sceneNumber);
    }*/
}
