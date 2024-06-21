using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHelth : MonoBehaviour
{
    public int Health;
    [SerializeField] private RectTransform healthIconHolder;
    [SerializeField] private GridLayoutGroup healthbar;
    [SerializeField] private GameObject healthBarGameObject;
    [SerializeField] private float positionY;
    public List<GameObject> healthPrefabs;
    [SerializeField] private GameObject healthIcon;
    void Start()
    {
        HelthIcons();
    }
    void Update()
    {
        healthBarGameObject.transform.position = new Vector2(healthBarGameObject.transform.position.x, positionY);
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        for (int i = 0; i < healthPrefabs.Count; i++)
        {
            healthPrefabs[i].SetActive(false);
        }
        healthPrefabs.Clear();
        HelthIcons();
    }
    private void HelthIcons()
    {
        for(int i = 0; i < Health; i++)
        {
            GameObject newIcon = Instantiate(healthIcon, healthIconHolder);
            healthPrefabs.Add(newIcon);
        }
    }
}
