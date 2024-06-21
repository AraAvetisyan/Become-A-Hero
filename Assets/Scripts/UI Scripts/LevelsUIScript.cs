using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System;

public class LevelsUIScript : MonoBehaviour
{
    [SerializeField] private GameObject pousePanel, settingsPanel, gameOverPanel;
    [SerializeField] private AudioSource soundtrackAudiosource;
    [SerializeField] private Slider audioSettingSlider;
    public bool gamePoused = false;
    private float lastVolume;
    private bool change = false;
    private int sceneIndex;
    //public static Action<bool> StopGameForAddRest;
    //[DllImport(dllName: "__Internal")]
    //private static extern bool ShowAdd();

    private void Start()
    {
        if (PlayerPrefs.GetInt("SoundOff") == 1)
        {
            soundtrackAudiosource.Stop();
        }
        else if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            soundtrackAudiosource.Play();
        }

        audioSettingSlider.value = PlayerPrefs.GetFloat("SetVolume");
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        gamePoused = false;
    }

    private void Update()
    {
        soundtrackAudiosource.volume = audioSettingSlider.value;

        if (gamePoused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        if (change)
        {
            audioSettingSlider.value = lastVolume;
            soundtrackAudiosource.Play();
            change = false;
        }
        
    }


    public void PouseGame()
    {
        gamePoused = true;
        pousePanel.SetActive(true);
    }
    public void ContinueGame()
    {
        gamePoused = false;
        pousePanel.SetActive(false);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(sceneIndex + 0);
     
    }
    public void SettingsPressed()
    {
        settingsPanel.SetActive(true);
        pousePanel.SetActive(false);
    }
    public void BackPressed()
    {
        settingsPanel.SetActive(false);
        pousePanel.SetActive(true);
        PlayerPrefs.SetFloat("SetVolume", audioSettingSlider.value);
    }
    public void PressedNextLevel()
    {
        if (sceneIndex == 15)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex + 1);
        }



    }
    public void PressedExit()
    {
        SceneManager.LoadScene(0);
    }
    public void PressedSoundOff()
    {
        PlayerPrefs.SetFloat("SetVolume", audioSettingSlider.value);
        PlayerPrefs.SetFloat("SetQuitVolume", audioSettingSlider.value);
        audioSettingSlider.value = 0;
        PlayerPrefs.SetInt("SoundOff", 1);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gamePoused = true;
    }
    public void PressedSoundOn()
    {
        if (PlayerPrefs.GetInt("SoundOff") == 1)
        {
            lastVolume = PlayerPrefs.GetFloat("SetQuitVolume");
            change = true;
        }
        audioSettingSlider.value = PlayerPrefs.GetFloat("SetVolume");
        PlayerPrefs.SetInt("SoundOff", 0);
        PlayerPrefs.SetFloat("SetQuitVolume", audioSettingSlider.value);

    }

}
