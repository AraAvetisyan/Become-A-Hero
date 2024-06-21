using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCombat : MonoBehaviour
{
    [SerializeField] private Animator playerAnimatorWithSpear;
    private int weaponState = 0;


    public bool IsAttacking = false;
    public bool IsRecharged = true;

    [SerializeField] private Transform spearAttackPoint;
    [SerializeField] private float spearAttackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;

    private void Awake()
    {
        IsRecharged = true;
    }
    void Start()
    {
        weaponState = PlayerPrefs.GetInt("Weapon");
    }
    void Update()
    {
        weaponState = PlayerPrefs.GetInt("Weapon");
       
    }
    private void OnEnable()
    {
        PlayerMovement.IsAttacking += Attack;
        AttackFromUI.SpearAttack += Attack;
    }
    private void OnDisable()
    {
        PlayerMovement.IsAttacking -= Attack;
        AttackFromUI.SpearAttack -= Attack;
    }
    private void Attack(bool attack)
    {
        if (attack && weaponState == 3 && IsRecharged)
        {
            playerAnimatorWithSpear.SetTrigger("IsAttacking");
            IsAttacking = true;
            IsRecharged = false;
            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCollDown());
        }
    }
    public void OnAttack()
    {
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(spearAttackPoint.position, spearAttackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(1);

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(spearAttackPoint.position, spearAttackRange);
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        IsAttacking = false;
    }
    private IEnumerator AttackCollDown()
    {
        yield return new WaitForSeconds(0.51f);
        IsRecharged = true;
    }
}