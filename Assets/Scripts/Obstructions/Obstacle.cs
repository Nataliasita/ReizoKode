using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    RunnerGameManager runnerGameManager;
    LifeController lifeController;

    private void Start()
    {
        runnerGameManager = FindObjectOfType<RunnerGameManager>();
        lifeController = FindObjectOfType<LifeController>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        { 
            lifeController.lifes--;
            if(lifeController.lifes == 0 ) 
            {
                Time.timeScale = 0;
                runnerGameManager.losePanel.SetActive(true);                
            }
        }
    }
}
