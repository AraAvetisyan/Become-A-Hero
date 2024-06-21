using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBossEnemyScript : MonoBehaviour
{
    private EnemyHelth _enemyHelth;
    [SerializeField] private Animator skeletonBossAnimator;
    private PlayerHealth _playerHealth;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float agroRange;
    [SerializeField] private float attackDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject finalBaner;
    private Vector3 startPosition;
    private Rigidbody2D rigidbody2D;
    private bool angry = false;
    private bool wait = false;
    private bool isFacingLeft = true;
    private bool toAttack;
    public bool waitToAttack;
    private bool isRecharged = true;
    private int stopChase = 0;
    public int attackCounter = 0;
    private bool isInDash = false;
    private bool isStunning = false;
    public bool canTakeDamage = false;
    [SerializeField] private bool dashing = false;
    [SerializeField] private bool deshLeft = false, deshRight = false;
    void Start()
    {
        _enemyHelth = GetComponent<EnemyHelth>();
        skeletonBossAnimator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _playerHealth = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();
    }


    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distToPlayer <= attackDistance && !waitToAttack && !isInDash && !isStunning)
        {
            toAttack = true;
            Attack();
        }
        else if (distToPlayer > attackDistance && !waitToAttack && !isInDash && !isStunning)
        {
            toAttack = false;
        }
        if (distToPlayer < agroRange && distToPlayer > attackDistance && !isInDash && !isStunning)
        {
            angry = true;
            ChasePlayer();
        }
        else if (distToPlayer > agroRange && !isInDash && !isStunning)
        {
            angry = false;
            StopChasingPlayer();

        }
        if (wait || waitToAttack && !isInDash && !isStunning)
        {
            Idle();
        }
        if (transform.position.x == startPosition.x && !isInDash && !isStunning)
        {
            Idle();
        }
        if (attackCounter == 3 && !isStunning && distToPlayer <= agroRange)
        {
            isInDash = true;
            if (transform.position.x < playerTransform.position.x)
            {
                deshRight = true;
                deshLeft = false;
                Debug.Log("Aj");
            }
            else if (transform.position.x > playerTransform.position.x)
            {
                deshLeft = true;
                deshRight = false;
                Debug.Log("Dzax");
                agroRange = 0;
                attackDistance = 0;
            }
            Dash();
        }
        if (isStunning)
        {
            Stun();
        }
        if (!dashing)
        {
            agroRange = 10;
            attackDistance = 2;
        }
        Flip();
        Die();
    }
    private void Stun()
    {
        canTakeDamage = true;
        skeletonBossAnimator.SetBool("IsStunning", true);
        skeletonBossAnimator.SetBool("IsInDash", false);
        StartCoroutine(StunnTimer());
        attackCounter = 0;
    }
    private void Dash()
    {
        dashing = true;
        moveSpeed = 2;
        agroRange = 0;
        attackDistance = 0;
        skeletonBossAnimator.SetFloat("Speed", 0);
        skeletonBossAnimator.SetBool("IsInDash", true);
        if (dashing)
        {
            
            if (deshLeft)
            {
                rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
            }
            if (deshRight)
            {
                rigidbody2D.velocity = new Vector2(moveSpeed, 0);
            }

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody && isInDash)
        {
            if (collision.rigidbody.GetComponent<PlayerHealth>())
            {
                attackCounter = 0;
                isInDash = false;
                skeletonBossAnimator.SetBool("IsInDash", false);
                skeletonBossAnimator.SetFloat("Speed", 1);
                dashing = false;
                ChasePlayer();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Column" && isInDash)
        {
            isInDash = false;
            skeletonBossAnimator.SetBool("IsInDash", false);
            dashing = false;
            isStunning = true;
        }
    }
    private void Attack()
    {
        rigidbody2D.velocity = Vector2.zero;
        if (toAttack)
        {
            if (isRecharged)
            {
                skeletonBossAnimator.SetTrigger("IsAttacking");
                isRecharged = false;
                StartCoroutine(AttackAnimation());
                StartCoroutine(AttackCollDown());
            }
            if (!isRecharged)
            {
                skeletonBossAnimator.SetFloat("Speed", 0f);
            }
        }
    }
    private void Flip()
    {
        if (isFacingLeft)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }

    }
    private void Idle()
    {
        skeletonBossAnimator.SetFloat("Speed", 0);
        moveSpeed = 0;

    }
    public void TakeDamageAfterAttac()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < attackDistance)
        {
            _playerHealth.TakeDamage(1);
        }
    }
    private void ChasePlayer()
    {

        stopChase = 1;
        moveSpeed = 3;
        skeletonBossAnimator.SetFloat("Speed", 1);
        wait = false;
        if (transform.position.x > playerTransform.position.x && angry)
        {
            isFacingLeft = true;
            rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x <= playerTransform.position.x && angry)
        {
            isFacingLeft = false;
            rigidbody2D.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }
    private void StopChasingPlayer()
    {
        rigidbody2D.velocity = Vector2.zero;
        if (stopChase == 1)
        {
            stopChase = 0;
            StartCoroutine(Wait());
        }
        if (!wait && stopChase == 0)
        {
            skeletonBossAnimator.SetFloat("Speed", 1);
            moveSpeed = 2;
            if (transform.position.x > startPosition.x)
            {
                isFacingLeft = true;

                transform.position = Vector2.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                transform.localScale = new Vector2(1, 1);
            }
            else if (transform.position.x > startPosition.x)
            {
                isFacingLeft = false;

                transform.position = Vector2.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                transform.localScale = new Vector2(-1, 1);
            }
        }
    }
    public void Die()
    {
        if (_enemyHelth.Health == 0)
        {
            moveSpeed = 0;
            skeletonBossAnimator.SetBool("IsDead", true);
            finalBaner.SetActive(true);
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public IEnumerator StunnTimer()
    {
        yield return new WaitForSecondsRealtime(3.5f);
        isStunning = false;
        skeletonBossAnimator.SetBool("IsStunning", false);
        canTakeDamage = false;
    }
    public IEnumerator Wait()
    {
        wait = true;
        yield return new WaitForSecondsRealtime(1.5f);
        stopChase = 0;
        wait = false;
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSecondsRealtime(0.9f);
        attackCounter += 1;
    }
    private IEnumerator AttackCollDown()
    {
        yield return new WaitForSecondsRealtime(1.75f);
        isRecharged = true;
    }
}
