using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComponentsFromOtherScenes : MonoBehaviour
{
    private const string UISCENENAME = "GameplayUIs";
    private const string SOUNDSSCENENAME = "Sounds";
    private event Action OnCompleteLoadScenes;

    private bool bothScenesLoaded = false;


    public void LoadScenes(Action OnComplete)
    {
        OnCompleteLoadScenes = OnComplete;
        StartCoroutine(LoadGameplayScene(UISCENENAME));
    }

    private IEnumerator LoadGameplayScene(string sceneName)
    {
        if (!IsSceneAlreadyLoaded(sceneName))
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
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

        if (!bothScenesLoaded)
        {
            bothScenesLoaded = true;
            StartCoroutine(LoadGameplayScene(SOUNDSSCENENAME));
        }
        else
            OnCompleteLoadScenes?.Invoke();
    }

    private bool IsSceneAlreadyLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
                return true;
        }

        return false;
    }
}
