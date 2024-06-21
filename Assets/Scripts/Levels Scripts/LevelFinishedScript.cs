using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishedScript : MonoBehaviour
{
    public static LevelFinishedScript instanse = null;
    private int sceneIndex;
    private int levelComplete;
    private int levelComleted;
    void Start()
    {
        if (instanse == null)
        {
            instanse = this;
        }
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelComleted = PlayerPrefs.GetInt("LevelCompleted");
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
        if (levelComleted < levelComplete)
        {

            PlayerPrefs.SetInt("LevelCompleted", levelComplete);
        }
    }
    public void GameEnds()
    {
        if (sceneIndex == 15)
        {
            PlayerPrefs.SetInt("LevelComplete", 15);
        }
        else
        {
            PlayerPrefs.SetInt("LevelComplete", sceneIndex + 1);
        }
    }
    public void isEndGame()
    {
        if (sceneIndex == 15)
        {
            Invoke("LoadMainMenu", 0.1f);
        }
        else
        {
            Invoke("NextLevel", 0.1f);
        }
    }
}
