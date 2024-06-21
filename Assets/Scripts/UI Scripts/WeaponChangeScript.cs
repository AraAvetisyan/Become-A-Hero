using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChangeScript : MonoBehaviour
{
    public void PressedNext()
    {
       
        if(PlayerPrefs.GetInt("Weapon") == 1 && PlayerPrefs.GetInt("WeaponCounter") == 1)
        {

        }

        if(PlayerPrefs.GetInt("Weapon") == 1 && PlayerPrefs.GetInt("WeaponCounter") == 2)
        {
            PlayerPrefs.SetInt("Weapon", 2);
        }
        else if (PlayerPrefs.GetInt("Weapon") == 2 && PlayerPrefs.GetInt("WeaponCounter") == 2)
        {
            PlayerPrefs.SetInt("Weapon", 1);
        }

        if (PlayerPrefs.GetInt("Weapon") == 1 && PlayerPrefs.GetInt("WeaponCounter") == 3)
        {
            PlayerPrefs.SetInt("Weapon", 2);
        }
        else if (PlayerPrefs.GetInt("Weapon") == 2 && PlayerPrefs.GetInt("WeaponCounter") == 3)
        {
            PlayerPrefs.SetInt("Weapon", 3);
        }
        else if (PlayerPrefs.GetInt("Weapon") == 3 && PlayerPrefs.GetInt("WeaponCounter") == 3)
        {
            PlayerPrefs.SetInt("Weapon", 1);
        }

    }
    public void PressedPrevious()
    {
        if (PlayerPrefs.GetInt("Weapon") == 1 && PlayerPrefs.GetInt("WeaponCounter") == 1)
        {

        }

        if (PlayerPrefs.GetInt("Weapon") == 1 && PlayerPrefs.GetInt("WeaponCounter") == 2)
        {
            PlayerPrefs.SetInt("Weapon", 2);
        }
        else if (PlayerPrefs.GetInt("Weapon") == 2 && PlayerPrefs.GetInt("WeaponCounter") == 2)
        {
            PlayerPrefs.SetInt("Weapon", 1);
        }

        if (PlayerPrefs.GetInt("Weapon") == 1 && PlayerPrefs.GetInt("WeaponCounter") == 3)
        {
            PlayerPrefs.SetInt("Weapon", 3);
        }
        else if (PlayerPrefs.GetInt("Weapon") == 2 && PlayerPrefs.GetInt("WeaponCounter") == 3)
        {
            PlayerPrefs.SetInt("Weapon", 1);
        }
        else if (PlayerPrefs.GetInt("Weapon") == 3 && PlayerPrefs.GetInt("WeaponCounter") == 3)
        {
            PlayerPrefs.SetInt("Weapon", 2);
        }

        
    }
}
