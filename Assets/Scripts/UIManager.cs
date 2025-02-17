using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject gamePanel;

    public bool isPaused = false;


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

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitToPlayGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

}
