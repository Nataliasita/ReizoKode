using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour
{
    public GameObject startScreenPanel;
    [SerializeField] private Text titleText;

    public void LevelTitle()
    {
        titleText.text = "LEVEL " + (RunnerGameManager.currentLevel + 1);
    }
}
