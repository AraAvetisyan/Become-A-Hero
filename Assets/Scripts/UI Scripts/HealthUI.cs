using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject HealthIconPrefab;
    [SerializeField] private GameObject HitHealth;
    [SerializeField] private PlayerHealth _playerHealth;
    private List<GameObject> healthIcons = new List<GameObject>();
    private List<GameObject> hitHealths = new List<GameObject>();
    public bool Heal = false;
    private void Update()
    {
        if (Heal == true)
        {
            for(int i = 0; i < 5; i++)
            {
                healthIcons[i].SetActive(false);
                hitHealths[i].SetActive(false);
            }
            
            healthIcons.Clear();
            hitHealths.Clear();
            Setap(_playerHealth.Health);
            Heal = false;
        }
    }
    public void Setap(int maxHealth)
    {
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newIcon = Instantiate(HealthIconPrefab, transform);
            healthIcons.Add(newIcon);
            GameObject newHitIcon = Instantiate(HitHealth, transform);
            hitHealths.Add(newHitIcon);
            hitHealths[i].SetActive(false);
        }

    }
    public void DisplayHealth(int health)
    {
        for (int i = 0; i < healthIcons.Count; i++)
        {
            if (i < health)
            {
                healthIcons[i].SetActive(true);
            }
            else
            {
                healthIcons[i].SetActive(false);
                hitHealths[i].SetActive(true);
            }
        }
        
    }
}