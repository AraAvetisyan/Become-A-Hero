using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowCombat : MonoBehaviour
{
    [SerializeField] private Animator plyaerAnimatorWithBow;
    private int weaponState = 0;

    public bool IsAttacking = false;
    public bool IsRecharged = true;
    public bool CanChangeWeapon = true;

    //
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private int arrowCount = 10;
    [SerializeField] private GameObject arrowPrefab;

    [SerializeField] private float arrowSpeed;


    private int counter = 0;
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
        if (counter == 1)
        {
            ArrowSpawn();
        }
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
        AttackFromUI.BowAttack += Attack;
    }
    private void OnDisable()
    {
        PlayerMovement.IsAttacking -= Attack;
        AttackFromUI.BowAttack -= Attack;
    }
    private void Attack(bool attack)
    {

        if (attack && weaponState == 2 && IsRecharged)
        {
            plyaerAnimatorWithBow.SetTrigger("IsAttacking");
            IsAttacking = true;
            IsRecharged = false;
            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCollDown());
            //rechardcounter = 1;
        }
    }
    private void ArrowSpawn()
    {
        counter = 0;
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
       // newArrow.GetComponent<Rigidbody2D>().velocity = arrowSpawnPoint.forward * arrowSpeed;
    }
    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(spearAttackPoint.position, swordAttackRange);
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        IsAttacking = false; 
        counter = 1;
    }
    private IEnumerator AttackCollDown()
    {
        yield return new WaitForSeconds(0.51f);
        IsRecharged = true;
    }
}