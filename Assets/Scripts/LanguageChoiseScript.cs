using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageChoiseScript : MonoBehaviour
{

    public void RusianLanguage()
    {
        PlayerPrefs.SetInt("language", 0); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void EnglishLanguage()
    {
        PlayerPrefs.SetInt("language", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
