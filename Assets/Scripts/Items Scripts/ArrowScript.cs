using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Rigidbody2D rigidbody2D;
    private bool flip = false;
    private float speed = 10f;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _playerMovement=FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        // Destroy(gameObject, 4f);
        if (_playerMovement.flip == -1)
        {
            flip = true;
        }
        if (flip)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void FixedUpdate()
    {
        if (!flip)
        {
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.GetComponent<EnemyHelth>())
    //    {
    //        collision.gameObject.GetComponent<EnemyHelth>().TakeDamage(1);
    //    }

    //    if (collision.gameObject.tag != "player")
    //    {
    //        Destroy(gameObject);
    //    }

    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.GetComponent<SkeletonBossEnemyScript>())
        {
            if (collision.GetComponent<SkeletonBossEnemyScript>().canTakeDamage == true)
            {
                Debug.Log("Kpav Bossin");
                collision.GetComponent<EnemyHelth>().TakeDamage(1);
            }
        }
        if (!collision.GetComponent<SkeletonBossEnemyScript>() && collision.gameObject.GetComponent<EnemyHelth>())
        {
            collision.GetComponent<EnemyHelth>().TakeDamage(1);
        }

        if (collision.gameObject.tag != "player" || collision.gameObject.tag != "Chest")
        {
            Destroy(gameObject);
        }
    }
}
