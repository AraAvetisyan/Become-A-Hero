using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyScript : MonoBehaviour
{
    private EnemyHelth _enemyHelth;
    private Animator slimeAnimator;


    [SerializeField] private Transform playerTransform;
    //[SerializeField] private Transform castPoint;
    [SerializeField] private float agroRange;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rigidbody2D;
    private Vector3 startTransformPosition;
    public bool angry = false;
    private bool isFacingLeft = true;
    void Start()
    {
        startTransformPosition = transform.position;
        _enemyHelth = GetComponent<EnemyHelth>();
        slimeAnimator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distToPlayer < agroRange)
        {
            angry = true;
            ChasePlayer();
        }
        else if (distToPlayer > agroRange)
        {
            angry = false;
            StopChasingPlayer();
        }

        //if (CanSeePlayer(agroRange))
        //{
        //    ChasePlayer();
        //}
        //else
        //{
        //    StopChasingPlayer();
        //}

        Die();

    }



    private void ChasePlayer()
    {
        if (transform.position.x > playerTransform.position.x && angry)
        {
            isFacingLeft = false;
            rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
            slimeAnimator.SetFloat("Speed", 1);
        }
        else if (transform.position.x <= playerTransform.position.x && angry)
        {
            isFacingLeft = true;
            rigidbody2D.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
            slimeAnimator.SetFloat("Speed", 1);
        }
    }
    //private bool CanSeePlayer(float distance)
    //{
    //    bool value = false;
    //    float castDist = distance;
    //    if (isFacingLeft)
    //    {
    //        castDist = -distance;
    //    }
    //    Vector2 endPos = castPoint.position + Vector3.left * castDist;
    //    RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));
    //    if (hit.collider != null)
    //    {
    //        if (hit.collider.gameObject.CompareTag("Player"))
    //        {
    //            value = true;
    //        }
    //        else
    //        {
    //            value = false;
    //        }
    //    }
    //    else
    //    {
    //        Debug.DrawLine(castPoint.position, endPos, Color.red);
    //    }
    //    return value;
    //}
    private void StopChasingPlayer()
    {
   
            rigidbody2D.velocity = Vector2.zero;
            slimeAnimator.SetFloat("Speed", 0);
          
    }
    public void Die()
    {
        if (_enemyHelth.Health == 0)
        {
            moveSpeed = 0;
            slimeAnimator.SetBool("IsDead", true);
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
