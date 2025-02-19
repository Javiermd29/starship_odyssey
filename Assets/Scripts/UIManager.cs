using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject mainGamePanel;
    public TextMeshProUGUI roundText;

    public bool isPaused = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one instance");
        }
        Instance = this;
    }

    private void Start()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        optionPanel.SetActive(false);
    }
    void Update()
    {
        // Detectar si se presiona la tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape) && gamePanel != null)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Método para pausar el juego
    public void PauseGame()
    {
        optionPanel.SetActive(true);
        gamePanel.SetActive(false);// Mostrar el panel
        Time.timeScale = 0f;        // Congelar el tiempo
        isPaused = true;
    }

    // Método para reanudar el juego
    public void ResumeGame()
    {
        optionPanel.SetActive(false);
        gamePanel.SetActive(true);// Ocultar el panel
        Time.timeScale = 1f;         // Reanudar el tiempo
        isPaused = false;
    }

    public void PlayGame()
    {
        StartCoroutine("WaitToPlayGame");
    }

    public void OptionPanel()
    {
        mainMenuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void MainMenu()
    {
        optionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void WinPanel()
    {
        mainGamePanel.SetActive(false);
        winPanel.SetActive(true);
    }

    public void LosePanel()
    {
        mainGamePanel.SetActive(false);
        losePanel.SetActive(true);
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene(2);
    }

    private IEnumerator WaitToPlayGame()
    {
        StartCoroutine(MusicManager.Instance.StartFade(1.5f, 0));
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }

}
