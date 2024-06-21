using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamageOnCollision : MonoBehaviour
{
    [SerializeField] private int damageValue = 1;
    private EnemyHelth _enemyHelth;
    private void Start()
    {
        _enemyHelth = GetComponent<EnemyHelth>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody)
        {
            if (collision.rigidbody.GetComponent<PlayerMovement>())
            {
                collision.rigidbody.GetComponent<PlayerMovement>().KBCounter = 0.2f;
                if (collision.rigidbody.transform.position.x <= transform.position.x)
                {
                    collision.rigidbody.GetComponent<PlayerMovement>().KnockFromRight = true;
                }
                if (collision.rigidbody.transform.position.x > transform.position.x)
                {
                    collision.rigidbody.GetComponent<PlayerMovement>().KnockFromRight = false;
                }
                if (collision.rigidbody.GetComponent<PlayerHealth>())
                {
                    if (_enemyHelth.Health != 0)
                    {
                        collision.rigidbody.GetComponent<PlayerHealth>().TakeDamage(damageValue);
                    }
                }
            }
            
        }
    }
}
