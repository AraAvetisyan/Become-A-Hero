using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel, levelsMenuPanel, forestLevelsPanel, caveLevelsPanel, iceForestLevelsPanel, settingsPanel;
    [SerializeField] private AudioSource soundtrackAudiosource;
    [SerializeField] private Slider audioSettingSlider;
    private bool gamePoused = false;
    private float lastVolume;
    private bool change = false;
    private int openLevel;

    private void Start()
    {
        PlayerPrefs.SetFloat("SetVolume", 1);
        if (PlayerPrefs.GetInt("SoundOff") == 1)
        {
            soundtrackAudiosource.Stop();
        }
        else if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            soundtrackAudiosource.Play();
        }

        audioSettingSlider.value = PlayerPrefs.GetFloat("SetVolume");

        openLevel = PlayerPrefs.GetInt("LevelCompleted");
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


    public void MenuSettingsPressed()
    {
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    public void MenuSettingsBackPressed()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        PlayerPrefs.SetFloat("SetVolume", audioSettingSlider.value);
    }
    public void MenuLevelsPressed()
    {
        levelsMenuPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    public void MenuLevelsBackPressed()
    {
        mainMenuPanel.SetActive(true);
        levelsMenuPanel.SetActive(false);
    }
    public void ForestButtonPressed()
    {
        levelsMenuPanel.SetActive(false);
        forestLevelsPanel.SetActive(true);
    }
    public void IceForestButtonPressed()
    {
        levelsMenuPanel.SetActive(false);
        iceForestLevelsPanel.SetActive(true);
    }
    public void CavesButtonPressed()
    {
        levelsMenuPanel.SetActive(false);
        caveLevelsPanel.SetActive(true);
    }
    public void ForestBackButtonPressed()
    {
        forestLevelsPanel.SetActive(false);
        levelsMenuPanel.SetActive(true);
    }
    public void IceForestBackButtonPressed()
    {
        iceForestLevelsPanel.SetActive(false);
        levelsMenuPanel.SetActive(true);
    }
    public void CavesBackButtonPressed()
    {
        caveLevelsPanel.SetActive(false);
        levelsMenuPanel.SetActive(true);
    }
    public void PressedPlayButton()
    {
        if (openLevel != 0)
        {
            SceneManager.LoadScene(openLevel);
        }
        else
        {
            SceneManager.LoadScene(1);
        }

    }
    public void PressedSoundOff()
    {
        PlayerPrefs.SetFloat("SetVolume", audioSettingSlider.value);
        PlayerPrefs.SetFloat("SetQuitVolume", audioSettingSlider.value);
        audioSettingSlider.value = 0;
        PlayerPrefs.SetInt("SoundOff", 1);
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
