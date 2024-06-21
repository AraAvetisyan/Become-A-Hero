using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static Action<bool> IsDead;
    private Animator playerAnimator;
    public int Health = 5;
    [SerializeField] private HealthUI _healthUI;
    [SerializeField] private GameObject gameOverPanel;
    public bool invulnerable = false;
    [SerializeField] private LevelsUIScript _levelsUIScript;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        _healthUI.Setap(Health);
        _healthUI.DisplayHealth(Health);
    }

    private void Update()
    {
        if (Health <= 0)
        {
            IsDead?.Invoke(true);
            playerAnimator.SetBool("IsDead", true);
        }
    }
    private void OnEnable()
    {
        HealthBottolScript.Heal += Heal;
        SceletonArrowScript.Damage += TakeDamage;
    }
    private void OnDisable()
    {
        HealthBottolScript.Heal -= Heal;
        SceletonArrowScript.Damage -= TakeDamage;
    }
    public void TakeDamage(int damageValue)
    {
        if (!invulnerable)
        {

            Health -= damageValue;
            if (Health <= 0)
            {
                Health = 0;
               
            }
            invulnerable = true;
            Invoke("StopInvulnerable", 0.5f);
            _healthUI.DisplayHealth(Health);
            // eventOnTakeDamage.Invoke();
        }
    }
    public void Heal(int health)
    {
        Health = health;
        _healthUI.Heal = true;
        //_healthUI.Setap(health);

    }
    private void StopInvulnerable()
    {
        invulnerable = false;
    }
    public void StopTheGame()
    {

        _levelsUIScript.gamePoused = true;
        gameOverPanel.SetActive(true);
    }

}