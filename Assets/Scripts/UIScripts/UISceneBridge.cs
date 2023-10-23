using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISceneBridge : MonoBehaviour
{
    public static GameplayUIs gameplayUis;
    [SerializeField] private GameplayUIs uisInScene;

    private void Awake()
    {
        gameplayUis = uisInScene;

        for (int i = 0; i < gameplayUis.tutorialTexts.Length; i++)
        {
            gameplayUis.tutorialTexts[i].enabled = false;
        }
    }

    [Serializable]
    public class GameplayUIs
    {
        public PauseManager pause;
        public RunnerProcessController process;
        public ScoreUIController score;
        public LifeController lifesController;
        public Text[] tutorialTexts;
    }
}

