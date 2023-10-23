using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button openPausePanel;
    [SerializeField] private Button MenuButton;

    private void Start()
    {
        PausePanel.SetActive(false);
        closeButton.onClick.AddListener(OnPressCloseButton);
        openPausePanel.onClick.AddListener(OnPressOpenPanelButton);
    }

    private void OnPressOpenPanelButton()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    private void OnPressCloseButton()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void MenuPause()
    {
        SceneManager.LoadScene("Main");
    }
}
