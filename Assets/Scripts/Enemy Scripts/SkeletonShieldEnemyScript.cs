using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShieldEnemyScript : MonoBehaviour
{
    private EnemyHelth _enemyHelth;
    [SerializeField] private Animator skeletonShieldAnimator;
    private PlayerHealth _playerHealth;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float agroRange;
    [SerializeField] private float attackDistance;
    [SerializeField] private float moveSpeed;
    private Vector3 startPosition;
    private Rigidbody2D rigidbody2D;
    private bool angry = false;
    private bool wait = false;
    private bool isFacingLeft = true;
    private bool toAttack;
    public bool waitToAttack;
    private bool isRecharged = true;
    private int stopChase = 0;
    void Start()
    {
        _enemyHelth = GetComponent<EnemyHelth>();
        skeletonShieldAnimator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _playerHealth = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distToPlayer <= attackDistance && !waitToAttack)
        {
            toAttack = true;
            Attack();
            //boarAnimator.SetTrigger("IsAttacking");
        }
        else
        {
            toAttack = false;
        }
        if (distToPlayer < agroRange && distToPlayer > attackDistance)
        {
            angry = true;
            ChasePlayer();
        }
        else if (distToPlayer > agroRange)
        {
            angry = false;
            StopChasingPlayer();

        }
        if (wait || waitToAttack)
        {
            Idle();
        }
        if (transform.position.x == startPosition.x)
        {
            Idle();
        }
        Flip();
        Die();

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
    private void Attack()
    {
        rigidbody2D.velocity = Vector2.zero;
        if (toAttack)
        {
            if (isRecharged)
            {
                skeletonShieldAnimator.SetTrigger("IsAttacking");
                isRecharged = false;
                StartCoroutine(AttackAnimation());
                StartCoroutine(AttackCollDown());
            }
            if (!isRecharged)
            {
                skeletonShieldAnimator.SetFloat("Speed", 0f);
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
        skeletonShieldAnimator.SetFloat("Speed", 0);
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
        moveSpeed = 1.5f;
        skeletonShieldAnimator.SetFloat("Speed", 1);
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
        //Idle();
        if (stopChase == 1)
        {
            stopChase = 0;
            StartCoroutine(Wait());
        }
        if (!wait && stopChase == 0)
        {
            skeletonShieldAnimator.SetFloat("Speed", 1);
            moveSpeed = 1.5f;
            if (transform.position.x > startPosition.x)
            {
                isFacingLeft = true;
                //rigidbody2D.velocity = new Vector2(-moveSpeed, 0);

                transform.position = Vector2.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                transform.localScale = new Vector2(1, 1);
            }
            else if (transform.position.x > startPosition.x)
            {
                isFacingLeft = false;
                //rigidbody2D.velocity = new Vector2(moveSpeed, 0);

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
            skeletonShieldAnimator.SetBool("IsDead", true);
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
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
        yield return new WaitForSecondsRealtime(0.41f);
    }
    private IEnumerator AttackCollDown()
    {
        yield return new WaitForSecondsRealtime(1.25f);
        isRecharged = true;
    }
}
