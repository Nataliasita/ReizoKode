using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundPanel : MonoBehaviour
{
    public static SoundPanel instance;

    public Slider _musicSlider, _sfxSlider;

    private void Awake()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("musicSave", 1f);
        _sfxSlider.value = PlayerPrefs.GetFloat("sfxSave", 1f);
    }
    private void Update()
    {
        MusicVolume();
        SFXVolume();
        SaveVolume();
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("musicSave", _musicSlider.value);
        PlayerPrefs.SetFloat("sfxSave", _sfxSlider.value);
    }

    public void MusicVolume()
    {
        SoundManager.instance.MusicVolume(_musicSlider.value);
    }
    public void SFXVolume()
    {
        SoundManager.instance.SFXVolume(_sfxSlider.value);
    }

}
