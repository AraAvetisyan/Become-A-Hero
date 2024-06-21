using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            PlayerPrefs.SetInt("Weapon", 1);
            PlayerPrefs.SetInt("SwordPickedUp", 1);
            PlayerPrefs.SetInt("WeaponCounter", 1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Bow")
        {
            PlayerPrefs.SetInt("Weapon", 2);
            PlayerPrefs.SetInt("BowPickedUp", 1);
            PlayerPrefs.SetInt("WeaponCounter", 2);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Spear")
        {
            PlayerPrefs.SetInt("Weapon", 3);
            PlayerPrefs.SetInt("SpearPickedUp", 1);
            PlayerPrefs.SetInt("WeaponCounter", 3);
            Destroy(collision.gameObject);
        }

    }
}
