using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerGameManager : MonoBehaviour
{
    [Header("Others")]
    [SerializeField] private ComponentsFromOtherScenes componentsFromOtherScenes;

    private void Start()
    {
        componentsFromOtherScenes.LoadScenes(OnLoadAllScenes);
    }

    private void OnLoadAllScenes()
    {
        
    }
}
