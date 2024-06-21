using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    private int levelCompleted;
    [SerializeField] private Button iceForestButton, cavesButton;

    [SerializeField]
    private Button forestLevel1, forestLevel2, forestLevel3, forestLevel4, forestLevel5;
    [SerializeField]
    private Button iceForestLevel1, iceForestLevel2, iceForestLevel3, iceForestLevel4, iceForestLevel5;
    [SerializeField]
    private Button cavesLevel1, cavesLevel2, cavesLevel3, cavesLevel4, cavesLevel5;
    [SerializeField]
    private GameObject lockIceForest, lockCaves;
    private void Start()
    {
        levelCompleted = PlayerPrefs.GetInt("LevelCompleted");
        iceForestButton.interactable = false;
        cavesButton.interactable = false;
        forestLevel2.interactable = false;
        forestLevel3.interactable = false;
        forestLevel4.interactable = false;
        forestLevel5.interactable = false;
        iceForestLevel1.interactable = false;
        iceForestLevel2.interactable = false;
        iceForestLevel3.interactable = false;
        iceForestLevel4.interactable = false;
        iceForestLevel5.interactable = false;
        cavesLevel1.interactable = false;
        cavesLevel2.interactable = false;
        cavesLevel3.interactable = false;
        cavesLevel4.interactable = false;
        cavesLevel5.interactable = false;
        switch (levelCompleted)
        {
            case 2:
                forestLevel2.interactable = true;
                break;
            case 3:
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;
            case 4:
                forestLevel4.interactable = true;
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;
            case 5:
                forestLevel5.interactable = true;
                forestLevel4.interactable = true;
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;
            case 6:
                lockIceForest.SetActive(false);
                iceForestLevel1.interactable = true;
                iceForestButton.interactable = true;
                forestLevel5.interactable = true;
                forestLevel4.interactable = true;
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;
            case 7:
                lockIceForest.SetActive(false);
                iceForestLevel2.interactable = true;
                iceForestLevel1.interactable = true;
                iceForestButton.interactable = true;
                forestLevel5.interactable = true;
                forestLevel4.interactable = true;
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;
            case 8:
                lockIceForest.SetActive(false);
                iceForestLevel3.interactable = true;
                iceForestLevel2.interactable = true;
                iceForestLevel1.interactable = true;
                iceForestButton.interactable = true;
                forestLevel5.interactable = true;
                forestLevel4.interactable = true;
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;
            case 9:
                lockIceForest.SetActive(false);
                iceForestLevel4.interactable = true;
                iceForestLevel3.interactable = true;
                iceForestLevel2.interactable = true;
                iceForestLevel1.interactable = true;
                iceForestButton.interactable = true;
                forestLevel5.interactable = true;
                forestLevel4.interactable = true;
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;
            case 10:
                lockIceForest.SetActive(false);
                iceForestLevel5.interactable = true;
                iceForestLevel4.interactable = true;
                iceForestLevel3.interactable = true;
                iceForestLevel2.interactable = true;
                iceForestLevel1.interactable = true;
                iceForestButton.interactable = true;
                forestLevel5.interactable = true;
                forestLevel4.interactable = true;
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;
            case 11:
                lockCaves.SetActive(false);
                lockIceForest.SetActive(false);
                cavesLevel1.interactable = true;
                cavesButton.interactable = true;
                iceForestLevel5.interactable = true;
                iceForestLevel4.interactable = true;
                iceForestLevel3.interactable = true;
                iceForestLevel2.interactable = true;
                iceForestLevel1.interactable = true;
                iceForestButton.interactable = true;
                forestLevel5.interactable = true;
                forestLevel4.interactable = true;
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;
            case 12:
                lockCaves.SetActive(false);
                lockIceForest.SetActive(false);
                cavesLevel2.interactable = true;
                cavesLevel1.interactable = true;
                cavesButton.interactable = true;
                iceForestLevel5.interactable = true;
                iceForestLevel4.interactable = true;
                iceForestLevel3.interactable = true;
                iceForestLevel2.interactable = true;
                iceForestLevel1.interactable = true;
                iceForestButton.interactable = true;
                forestLevel5.interactable = true;
                forestLevel4.interactable = true;
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;
            case 13:
                lockCaves.SetActive(false);
                lockIceForest.SetActive(false);
                cavesLevel3.interactable = true;
                cavesLevel2.interactable = true;
                cavesLevel1.interactable = true;
                cavesButton.interactable = true;
                iceForestLevel5.interactable = true;
                iceForestLevel4.interactable = true;
                iceForestLevel3.interactable = true;
                iceForestLevel2.interactable = true;
                iceForestLevel1.interactable = true;
                iceForestButton.interactable = true;
                forestLevel5.interactable = true;
                forestLevel4.interactable = true;
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;
            case 14:
                lockCaves.SetActive(false);
                lockIceForest.SetActive(false);
                cavesLevel4.interactable = true;
                cavesLevel3.interactable = true;
                cavesLevel2.interactable = true;
                cavesLevel1.interactable = true;
                cavesButton.interactable = true;
                iceForestLevel5.interactable = true;
                iceForestLevel4.interactable = true;
                iceForestLevel3.interactable = true;
                iceForestLevel2.interactable = true;
                iceForestLevel1.interactable = true;
                iceForestButton.interactable = true;
                forestLevel5.interactable = true;
                forestLevel4.interactable = true;
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;
            case 15:
                lockCaves.SetActive(false);
                lockIceForest.SetActive(false);
                cavesLevel5.interactable = true;
                cavesLevel4.interactable = true;
                cavesLevel3.interactable = true;
                cavesLevel2.interactable = true;
                cavesLevel1.interactable = true;
                cavesButton.interactable = true;
                iceForestLevel5.interactable = true;
                iceForestLevel4.interactable = true;
                iceForestLevel3.interactable = true;
                iceForestLevel2.interactable = true;
                iceForestLevel1.interactable = true;
                iceForestButton.interactable = true;
                forestLevel5.interactable = true;
                forestLevel4.interactable = true;
                forestLevel3.interactable = true;
                forestLevel2.interactable = true;
                break;         
                
        }
    }
    public void LoadTo(int level)
    {
        SceneManager.LoadScene(level);
    }

}
