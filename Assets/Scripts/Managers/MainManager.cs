using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button buttonStartGame;
    [SerializeField] private String sceneToLoad;

    void Awake()
    {
        buttonStartGame.onClick.AddListener(OnPressStartGame);
    }
    private void OnPressStartGame()
    {
        StartCoroutine(LoadGameplayScene());        
    }

    private IEnumerator LoadGameplayScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        loadOperation.allowSceneActivation = false;

        while (!loadOperation.isDone)
        {
            if (loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
