using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCombat : MonoBehaviour
{
    [SerializeField] private Animator playerAnimatorWithSword;
    private int weaponState = 0;


    public bool IsAttacking = false;
    public bool IsRecharged = true;
    public bool CanChangeWeapon = true;

    [SerializeField] private Transform swordAttackPoint;
    [SerializeField] private float swordAttackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;

    //[SerializeField] List<EnemyHealth> enemies;
    //[SerializeField] List<GameObject> enemyObjects;
    private void Awake()
    {
        IsRecharged = true;
       // playerAnimatorWithSword = GetComponent<Animator>();
    }
    void Start()
    {
        weaponState = PlayerPrefs.GetInt("Weapon");
    }

    void Update()
    {
        weaponState = PlayerPrefs.GetInt("Weapon");
        if (IsAttacking)
        {
            CanChangeWeapon = false;
        }
        else
        {
            CanChangeWeapon = true;
        }
    }
    private void OnEnable()
    {
        PlayerMovement.IsAttacking += Attack;
        AttackFromUI.SwordAttack += Attack;
    }
    private void OnDisable()
    {
        PlayerMovement.IsAttacking -= Attack;
        AttackFromUI.SwordAttack -= Attack;
    }
    private void Attack(bool attack)
    {
        if (attack && weaponState == 1 && IsRecharged)
        {
            playerAnimatorWithSword.SetTrigger("IsAttacking");
            IsAttacking = true;
            IsRecharged = false;
            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCollDown());
            attack = false;
        }
    }
    public void OnAttack()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(swordAttackPoint.position, swordAttackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {

            // enemy.GetComponent<EnemyHealth>().TakeDamage(1);

            if (enemy.GetComponent<SkeletonBossEnemyScript>())
            {
                if (enemy.GetComponent<SkeletonBossEnemyScript>().canTakeDamage)
                {
                    enemy.GetComponent<EnemyHelth>().TakeDamage(1);
                }
            }
            if (!enemy.GetComponent<SkeletonBossEnemyScript>() && enemy.gameObject.GetComponent<EnemyHelth>())
            {
                enemy.GetComponent<EnemyHelth>().TakeDamage(1);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(swordAttackPoint.position, swordAttackRange);
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.31f);
        IsAttacking = false;
    }
    private IEnumerator AttackCollDown()
    {
        yield return new WaitForSeconds(0.33f);
        IsRecharged = true;
    }
}