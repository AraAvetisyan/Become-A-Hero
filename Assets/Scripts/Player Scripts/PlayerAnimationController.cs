using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimatorWithoutWeapon, playerAnimatorWithSword, playerAnimatorWithBow, playerAnimatorWithSpear;
    [SerializeField] private GameObject playerWithoutWeapon, playerWithSword, playerWithBow, playerWithSpear;
    private int weaponState = 0;


    private void Awake()
    {
        weaponState = PlayerPrefs.GetInt("Weapon");
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        weaponState = PlayerPrefs.GetInt("Weapon");
        if (weaponState == 0)
        {
            playerWithoutWeapon.SetActive(true);
            playerWithSword.SetActive(false);
            playerWithBow.SetActive(false);
            playerWithSpear.SetActive(false);
        }
        else if (weaponState == 1)
        {
            playerWithoutWeapon.SetActive(false);
            playerWithSword.SetActive(true);
            playerWithBow.SetActive(false);
            playerWithSpear.SetActive(false);
        }
        else if (weaponState == 2)
        {
            playerWithoutWeapon.SetActive(false);
            playerWithSword.SetActive(false);
            playerWithBow.SetActive(true);
            playerWithSpear.SetActive(false);
        }
        else if (weaponState == 3)
        {
            playerWithoutWeapon.SetActive(false);
            playerWithSword.SetActive(false);
            playerWithBow.SetActive(false);
            playerWithSpear.SetActive(true);
        }
    }
    private void OnEnable()
    {
        PlayerMovement.IsRunning += Running;
        PlayerMovement.IsJumping += Jumping;
        PlayerHealth.IsDead += Dead;
    }
    private void OnDisable()
    {
        PlayerMovement.IsRunning -= Running;
        PlayerMovement.IsJumping -= Jumping;
        PlayerHealth.IsDead -= Dead;
    }
    
    private void Running(bool isRunning)
    {
        if (isRunning && weaponState == 0)
        {
            playerAnimatorWithoutWeapon.SetFloat("Speed", 1f);
        }
        else if (!isRunning && weaponState == 0)
        {
            playerAnimatorWithoutWeapon.SetFloat("Speed", 0f);
        }
        if (isRunning && weaponState == 1)
        {
            playerAnimatorWithSword.SetFloat("Speed", 1f);
        }
        else if (!isRunning && weaponState == 1)
        {
            playerAnimatorWithSword.SetFloat("Speed", 0f);
        }
        if (isRunning && weaponState == 2)
        {
            playerAnimatorWithBow.SetFloat("Speed", 1f);
        }
        else if (!isRunning && weaponState == 2)
        {
            playerAnimatorWithBow.SetFloat("Speed", 0f);
        }
        if (isRunning && weaponState == 3)
        {
            playerAnimatorWithSpear.SetFloat("Speed", 1f);
        }
        else if (!isRunning && weaponState == 3)
        {
            playerAnimatorWithSpear.SetFloat("Speed", 0f);
        }
    }
    private void Jumping(bool isJumping)
    {
        if (isJumping && weaponState == 0)
        {
            playerAnimatorWithoutWeapon.SetBool("IsJumping", true);
        }
        else if (!isJumping && weaponState == 0)
        {
            playerAnimatorWithoutWeapon.SetBool("IsJumping", false);
        }
        if (isJumping && weaponState == 1)
        {
            playerAnimatorWithSword.SetBool("IsJumping", true);
        }
        else if (!isJumping && weaponState == 1)
        {
            playerAnimatorWithSword.SetBool("IsJumping", false);
        }
        if (isJumping && weaponState == 2)
        {
            playerAnimatorWithBow.SetBool("IsJumping", true);
        }
        else if (!isJumping && weaponState == 2)
        {
            playerAnimatorWithBow.SetBool("IsJumping", false);
        }
        if (isJumping && weaponState == 3)
        {
            playerAnimatorWithSpear.SetBool("IsJumping", true);
        }
        else if (!isJumping && weaponState == 3)
        {
            playerAnimatorWithSpear.SetBool("IsJumping", false);
        }
    }
    private void Dead(bool isdead)
    {
        if (isdead)
        {
         //   _levelsUIScript.gamePoused = true;
        }
        if (isdead && weaponState == 0)
        {
            playerAnimatorWithoutWeapon.SetBool("IsDead", true);
        }
        else if (!isdead && weaponState == 0)
        {
            playerAnimatorWithoutWeapon.SetBool("IsDead", false);
        }
        if (isdead && weaponState == 1)
        {
            playerAnimatorWithSword.SetBool("IsDead", true);
        }
        else if (!isdead && weaponState == 1)
        {
            playerAnimatorWithSword.SetBool("IsDead", false);
        }
        if (isdead && weaponState == 2)
        {
            playerAnimatorWithBow.SetBool("IsDead", true);
        }
        else if (!isdead && weaponState == 2)
        {
            playerAnimatorWithBow.SetBool("IsDead", false);
        }
        if (isdead && weaponState == 3)
        {
            playerAnimatorWithSpear.SetBool("IsDead", true);
        }
        else if (!isdead && weaponState == 3)
        {
            playerAnimatorWithSpear.SetBool("IsDead", false);
        }
    }
}
