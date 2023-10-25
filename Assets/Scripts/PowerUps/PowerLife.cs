using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLife : MonoBehaviour
{
    private SoundManager soundManager;
    LifeController lifeController;
    private void Awake(){
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        lifeController = FindObjectOfType<LifeController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {   
            lifeController.lifes++;
            soundManager.PlaySFX("PowerUp");
            Destroy(gameObject);
        }
        
    }

}
