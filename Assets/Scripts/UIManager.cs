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
        // Detect if ESC  is pressed
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

    // Pause the game
    public void PauseGame()
    {
        optionPanel.SetActive(true);
        gamePanel.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Resume the game
    public void ResumeGame()
    {
        optionPanel.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 1f;
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
        StartCoroutine(ShowWinPanelCoroutine());
    }

    // Coroutine to wait X seconds to show the win panel
    private IEnumerator ShowWinPanelCoroutine()
    {
        yield return new WaitForSeconds(2.8f);
        mainGamePanel.SetActive(false);
        winPanel.SetActive(true);
    }

    public void LosePanel()
    {
        StartCoroutine(ShowLosePanelCoroutine());
    }

    // Coroutine to wait X seconds to show the lose panel
    private IEnumerator ShowLosePanelCoroutine()
    {
        yield return new WaitForSeconds(4.5f);
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

    // Coroutine to wait X seconds to play the game
    private IEnumerator WaitToPlayGame()
    {
        StartCoroutine(MusicManager.Instance.StartFade(1.5f, 0));
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }

}
