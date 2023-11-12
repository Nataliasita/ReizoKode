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
    // [SerializeField] private GameObject playerCharacter;
    [SerializeField] private GameObject playerPanel;

    public List<Characters> characters;

    [Header("Win and Lose Panels")]
    [SerializeField] private GameObject winPanel;

    public GameObject prefabPlayerBase; 
    public GameObject prefabPlayer2; 
    public GameObject prefabPlayer3; 
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button restartWinButton;
    [SerializeField] private Button restartLoseButton;
    [SerializeField] private Button playButton;

    public GameObject losePanel;

    private bool prefabInstantiatedDam = false;

    public GameObject prefabDam; 

    private SoundManager soundManager;

    [Header("Other Components")]
    [SerializeField] private GroundSpawner groundSpawner;
    [SerializeField] private LifeController lifeController;
    [SerializeField] private StartScreenController startScreen;

    public int numberPlayer;
    
    private bool levelCompleted = false;
    public static int currentLevel = 0;

    private void Awake()
    {        
        playerPanel.SetActive(true);
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        OnLoadAllScenes();
        soundManager.PlayMusic("Music Level");
        soundManager.MusicVolume(0.1f);
        InvokeRepeating("PlaySoundAmbientDay", 0f, 25f);
    }

    void Update()
    {
        if(numberPlayer == 0){
            prefabPlayerBase.SetActive(true);
            prefabPlayer2.SetActive(false);
            prefabPlayer3.SetActive(false);
        }
        else if(numberPlayer == 1)
        {
            prefabPlayer2.SetActive(true);
            prefabPlayerBase.SetActive(false);
            prefabPlayer3.SetActive(false);
        }
        else if(numberPlayer == 2)
        {
            prefabPlayer3.SetActive(true);
            prefabPlayerBase.SetActive(false);
            prefabPlayer2.SetActive(false);
        }
    }

    private void PlaySoundAmbientDay(){
        soundManager.PlaySFX("Day");
        soundManager.LoopMusic = true;
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
        
        if (!prefabInstantiatedDam && level < groundSpawner.groundTilePrefabs.Count)
        {
            groundSpawner.SetCurrentLevel(level);

            progressBar.maxValue = levelDuration;
            progressBar.value = 0;       

            StartCoroutine(LevelTimer());

            Vector3 spawnPosition = new Vector3(-44.516f, 14f, 4148f);
            Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);

            Instantiate(prefabDam, spawnPosition, spawnRotation);

            prefabInstantiatedDam = true;
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
        // groundSpawner.DestroyPreviousLevelTiles();
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
        // groundSpawner.DestroyPreviousLevelTiles();
        groundSpawner.SetCurrentLevel(level);
        groundSpawner.StartSpawning();

        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    IEnumerator 
    
    
    LevelTimer()
    {
        float timeElapsed = 0f;
        bool timerSoundPlayed = false;    

        while (timeElapsed < levelDuration)
        {
            progressBar.value = timeElapsed;
            timeElapsed += Time.deltaTime;
            int eightyPercent = (int)(0.9f * levelDuration);
            if (timeElapsed >= eightyPercent && !timerSoundPlayed)
            {
                soundManager.PlaySFX("Timer");
                timerSoundPlayed = true;
            }
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
                soundManager.PlayMusic("Music Win");
            }
        });

        Debug.Log("Has completado el nivel " + currentLevel);
    }
}
