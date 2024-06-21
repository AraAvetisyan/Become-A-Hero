using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBottolScript : MonoBehaviour
{
    public static Action<int> Heal;
    [SerializeField] PlayerHealth _playerHealth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Heal?.Invoke(5);
            Destroy(gameObject);
        }

    }
}
