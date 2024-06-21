using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFromUI : MonoBehaviour
{
    public static Action<bool> SwordAttack;
    public static Action<bool> BowAttack;
    public static Action<bool> SpearAttack;
    public void Attack()
    {
        if (PlayerPrefs.GetInt("Weapon") == 1)
        {
            SwordAttack?.Invoke(true);
        }
        if(PlayerPrefs.GetInt("Weapon") == 2)
        {
            BowAttack?.Invoke(true);
        }
        if(PlayerPrefs.GetInt("Weapon") == 3)
        {
            SpearAttack?.Invoke(true);
        }
    }
}
