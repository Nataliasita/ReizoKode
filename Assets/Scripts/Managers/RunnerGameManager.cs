using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunnerGameManager : MonoBehaviour
{
    [Header("Level Manager")]
    public Transform levelManager;

    [Header("Progress")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private float levelDuration = 60f;

    [Header("Others")]
    [SerializeField] private GameObject playerCharacter;

    [Header("Win and Lose Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button restartWinButton;
    [SerializeField] private Button restartLoseButton;
    [SerializeField] private Button playButton;

    public GameObject losePanel;

    [Header("Other Components")]
    [SerializeField] private GroundSpawner groundSpawner;
    [SerializeField] private LifeController lifeController;
    [SerializeField] private StartScreenController startScreen;

    private bool levelCompleted = false;
    public static int currentLevel = 0;

    private void Awake()
    {        
        OnLoadAllScenes();

    }

    private void OnLoadAllScenes()
    {        
        startScreen.startScreenPanel.SetActive(true);

        LoadLevel(currentLevel);

        playButton.onClick.AddListener(() =>
        {
            OnPressPlayButton();
        });

        restartLoseButton.onClick.AddListener(() =>
        {
            levelCompleted = false;
            RestartLoseLevel(currentLevel);
        });

        restartWinButton.onClick.AddListener(() =>
        {
            levelCompleted = false;
            RestartWinLevel(currentLevel);
        });
    }

    private void OnPressPlayButton() 
    {
        Time.timeScale = 1;
        startScreen.startScreenPanel.SetActive(false);
    }

    private void LoadLevel(int level)
    {
        winPanel.SetActive(false); 
        losePanel.SetActive(false);

   
        if (level < groundSpawner.groundTilePrefabs.Count)
        {
            groundSpawner.SetCurrentLevel(level);

            progressBar.maxValue = levelDuration;
            progressBar.value = 0;            

            StartCoroutine(LevelTimer());
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    private void RestartWinLevel(int level)
    {
        Time.timeScale = 1;
        lifeController.lifes = 3;
        LoadLevel(level - 1);
        groundSpawner.DestroyPreviousLevelTiles();
        groundSpawner.SetCurrentLevel(level - 1);
        groundSpawner.StartSpawning();

        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }


    private void RestartLoseLevel(int level)
    {
        Time.timeScale = 1;
        lifeController.lifes = 3;
        LoadLevel(level);
        groundSpawner.DestroyPreviousLevelTiles();
        groundSpawner.SetCurrentLevel(level);
        groundSpawner.StartSpawning();

        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    IEnumerator LevelTimer()
    {
        float timeElapsed = 0f;

        while (timeElapsed < levelDuration)
        {
            progressBar.value = timeElapsed;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (!levelCompleted)
        {
            currentLevel++;
            levelCompleted = true;
        }

        Time.timeScale = 0;
        winPanel.SetActive(true);

        nextLevelButton.onClick.AddListener(() =>
        {
            startScreen.startScreenPanel.SetActive(true);
            levelCompleted = false;
            LoadLevel(currentLevel);
            groundSpawner.DestroyPreviousLevelTiles();
            groundSpawner.StartSpawning();
            Time.timeScale = 0;

            if (currentLevel >= groundSpawner.groundTilePrefabs.Count)
            {
                Time.timeScale = 0;
                startScreen.startScreenPanel.SetActive(false);
                SceneManager.LoadScene("EndScene");
            }
        });

        Debug.Log("Has completado el nivel " + currentLevel);
    }
}
