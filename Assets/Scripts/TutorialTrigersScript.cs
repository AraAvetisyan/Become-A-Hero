using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigersScript : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;
    [SerializeField] private int triggerCounter = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && triggerCounter == 0)
        {
            tutorial.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && triggerCounter == 0)
        {
            tutorial.SetActive(false);
            triggerCounter++;
        }
    }
}
