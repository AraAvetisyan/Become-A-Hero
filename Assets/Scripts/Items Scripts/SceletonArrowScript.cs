using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceletonArrowScript : MonoBehaviour
{
    private Rigidbody2D rigidbody2D; 
    private bool flip = false;
    private float speed = 10f;
    private SpriteRenderer spriteRenderer;
    //[SerializeField] private SkeletonArcherEnemyScript _skeletonArcherEnemyScript;
    public static Action<int> Damage;
    // [SerializeField] private PlayerHealth _playerHealth;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (transform.parent.tag=="LeftSP")
        {
            spriteRenderer.flipX = true;
            flip = true;
            Debug.Log("Nety Dzaxic");
        }
        if(transform.parent.tag=="RightSP")
        {
            spriteRenderer.flipX = false;
            flip = false;
            Debug.Log("Nety Ajic");
        }
    }

    private void FixedUpdate()
    {
        if (transform.parent.tag == "RightSP")
        {
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
          //  flip = false;
        }
        if (transform.parent.tag == "LeftSP")
        {
            rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
          //  flip = true;
        }      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
              
                Destroy(gameObject);
            
           
        }
        if (collision.gameObject.tag == "Player")
        {

            Destroy(gameObject);
            
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {

            Destroy(gameObject);


        }
        if (collision.gameObject.tag == "Player")
        {

            Destroy(gameObject);
            Damage?.Invoke(1);
        }
    }
}
