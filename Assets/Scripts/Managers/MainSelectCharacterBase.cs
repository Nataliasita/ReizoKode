using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainSelectCharacterBase : MonoBehaviour
{

    
    private int index;
    public Image image;
    [SerializeField] public TextMeshProUGUI name;
    public TextMeshProUGUI power;

    [SerializeField] private RunnerGameManager RunnerGameManager;

    private void Awake(){
        index = PlayerPrefs.GetInt("IndexCharacter");

        if(index > RunnerGameManager.characters.Count - 1){
            index = 0;
        }

        ChangeView();

    }

    private void ChangeView(){
        PlayerPrefs.SetInt("IndexCharacter", index);
        image.sprite = RunnerGameManager.characters[index].image;
        name.text = RunnerGameManager.characters[index].name;
        power.text = RunnerGameManager.characters[index].power;
    }

    public void NextCharacter(){
        if(index == RunnerGameManager.characters.Count -1){
            index = 0;
        }else{
            index += 1;
        }
        ChangeView();
    }

    public void PreviousCharacter(){
        if(index == 0){
            index = RunnerGameManager.characters.Count -1;
        }else{
            index -= 1;
        }
        ChangeView();
    }

    public void SelectedStart(){
        RunnerGameManager.numberPlayer = index;
        Destroy(gameObject);
    }

}
