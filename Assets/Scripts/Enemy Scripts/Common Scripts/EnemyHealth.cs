using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //public static Action<bool> IsDead;
    public int Health;

    void Start()
    {
        
    }


    void Update()
    {
        if (Health <= 0)
        {
            Health = 0;
       //     IsDead?.Invoke(true);
        }
    }
    public void TakeDamage(int damageValue)
    {       
        Health -= damageValue;
    }
}
