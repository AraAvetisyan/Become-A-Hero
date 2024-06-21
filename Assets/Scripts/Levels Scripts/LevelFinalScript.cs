using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinalScript : MonoBehaviour
{
    [SerializeField] private GameObject FinalPanel;
    [SerializeField] private LevelFinishedScript _levelFinishedScript;
    [SerializeField] private LevelsUIScript _levelsUIScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _levelFinishedScript.GameEnds();
            FinalPanel.SetActive(true);
            _levelsUIScript.gamePoused = true;

            Time.timeScale = 0f;
        }
    }
}
