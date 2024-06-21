using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcherEnemyScript : MonoBehaviour
{
    private EnemyHelth _enemyHelth;
    [SerializeField] private Animator skeletonArcherAnimator;
    private PlayerHealth _playerHealth;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float agroRange;
    [SerializeField] private float attackDistance;
    [SerializeField] private float moveSpeed;
    private Vector3 startPosition;
    private Rigidbody2D rigidbody2D;
    private bool angry = false;
    private bool wait = false;
    public static bool IsFacingLeft = false;
    private bool toAttack;
    public bool waitToAttack;
    private bool isRecharged = true;
    private int stopChase = 0;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowRightSpownPoint, arrowLeftSpawnPoint;
    [SerializeField] private SpriteRenderer SkeletonSprite;
    void Start()
    {
        _enemyHelth = GetComponent<EnemyHelth>();
        skeletonArcherAnimator = GetComponent<Animator>();
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
    public void ArrowInstantiete()
    {
        arrow.SetActive(true);
        if (playerTransform.position.x < transform.position.x)
        {
            Instantiate(arrow, arrowLeftSpawnPoint);
        }
        else if (playerTransform.position.x > transform.position.x)
        {
            Instantiate(arrow, arrowRightSpownPoint);
        }
    }
    private void Attack()
    {
        rigidbody2D.velocity = Vector2.zero;
        if (toAttack)
        {
            if (isRecharged)
            {
                skeletonArcherAnimator.SetTrigger("IsAttacking");
                isRecharged = false;
                StartCoroutine(AttackAnimation());
                StartCoroutine(AttackCollDown());
            }
            if (!isRecharged)
            {
                skeletonArcherAnimator.SetFloat("Speed", 0f);
            }
        }
    }
    private void Flip()
    {
        if (transform.position.x > playerTransform.position.x)
        {
            SkeletonSprite.flipX = true;
            IsFacingLeft = true;
        }
        else
        {
            SkeletonSprite.flipX = false;
            IsFacingLeft = false;
        }

    }
    private void Idle()
    {
        skeletonArcherAnimator.SetFloat("Speed", 0);
        moveSpeed = 0;

    }
    
    private void ChasePlayer()
    {

        stopChase = 1;
        moveSpeed = 1.5f;
        skeletonArcherAnimator.SetFloat("Speed", 1);
        wait = false;
        if (transform.position.x > playerTransform.position.x && angry)
        {
            IsFacingLeft = true;
            rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
           // transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x <= playerTransform.position.x && angry)
        {
            IsFacingLeft = false;
            rigidbody2D.velocity = new Vector2(moveSpeed, 0);
          //  transform.localScale = new Vector2(-1, 1);
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
            skeletonArcherAnimator.SetFloat("Speed", 1);
            moveSpeed = 1.5f;
            if (transform.position.x > startPosition.x)
            {
                IsFacingLeft = true;
                //rigidbody2D.velocity = new Vector2(-moveSpeed, 0);

                transform.position = Vector2.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
              //  transform.localScale = new Vector2(1, 1);
            }
            else if (transform.position.x > startPosition.x)
            {
                IsFacingLeft = false;
                //rigidbody2D.velocity = new Vector2(moveSpeed, 0);

                transform.position = Vector2.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
             //   transform.localScale = new Vector2(-1, 1);
            }
        }
    }
    public void Die()
    {
        if (_enemyHelth.Health == 0)
        {
            moveSpeed = 0;
            skeletonArcherAnimator.SetBool("IsDead", true);
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
        yield return new WaitForSecondsRealtime(2.5f);
        isRecharged = true;
    }
}