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

    private void Start()
    {
        mainMenuPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
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

    

}
