using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinRiderEnemyScript : MonoBehaviour
{
    private EnemyHelth _enemyHelth;
    private Animator goblinRiderAnimator;
    private PlayerHealth _playerHealth;

    [SerializeField] private Transform playerTransform;
    private Vector3 nextPosition;

    [SerializeField] private float agroRange;
    [SerializeField] private float attackDistance;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rigidbody2D;
    private Vector3 leftPatrolPosition;
    private Vector3 rightPatrolPosition;
    private bool angry = false;
    private bool wait = false;
    private bool isFacingLeft = true;
    private int stopChase = 0;
    private bool toAttack;
    public bool waitToAttack;
    private bool isRecharged = true;
    private
    void Start()
    {
        _enemyHelth = GetComponent<EnemyHelth>();
        goblinRiderAnimator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        leftPatrolPosition = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
        rightPatrolPosition = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
        nextPosition = leftPatrolPosition;
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
            stopChase = 1;
            ChasePlayer();
        }
        else if (distToPlayer > agroRange)
        {

            angry = false;
            if (stopChase == 1)
            {
                StopChasingPlayer();
                nextPosition = leftPatrolPosition;

            }
            if (!wait && stopChase == 0)
            {

                Patrol();
            }
        }
        if (wait || waitToAttack)
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
                goblinRiderAnimator.SetTrigger("IsAttacking");
                isRecharged = false;
                StartCoroutine(AttackAnimation());
                StartCoroutine(AttackCollDown());
            }
            if (!isRecharged)
            {
                goblinRiderAnimator.SetFloat("Speed", 0f);
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
    private void Patrol()
    {
        moveSpeed = 1.5f;
        transform.position = Vector2.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        if (!wait)
        {
            goblinRiderAnimator.SetFloat("Speed", 1);
            goblinRiderAnimator.SetBool("IsRunning", false);
        }
        if (transform.position.x == leftPatrolPosition.x && !wait)
        {
            StartCoroutine(Wait());
            nextPosition = rightPatrolPosition;
        }
        else if (transform.position.x == rightPatrolPosition.x && !wait)
        {
            StartCoroutine(Wait());
            nextPosition = leftPatrolPosition;
        }

    }
    private void Idle()
    {

        goblinRiderAnimator.SetFloat("Speed", 0);
        goblinRiderAnimator.SetBool("IsRunning", false);
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

        moveSpeed = 3;
        goblinRiderAnimator.SetFloat("Speed", 1);
        goblinRiderAnimator.SetBool("IsRunning", true);
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
        Idle();
        StartCoroutine(Wait());

    }
    public void Die()
    {
        if (_enemyHelth.Health == 0)
        {
            moveSpeed = 0;
            goblinRiderAnimator.SetBool("IsDead", true);
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
        wait = false;
        stopChase = 0;
        if (nextPosition.x == leftPatrolPosition.x)
        {
            isFacingLeft = true;
        }
        else if (nextPosition.x == rightPatrolPosition.x)
        {
            isFacingLeft = false;
        }
        if (nextPosition == leftPatrolPosition && transform.position.x < leftPatrolPosition.x)
        {
            isFacingLeft = false;
        }
        else if (nextPosition == rightPatrolPosition && transform.position.x > rightPatrolPosition.x)
        {
            isFacingLeft = true;
        }
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
