using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 position;
    private float speed = 2.5f;
    private void Awake()
    {
        if (!playerTransform)
        {
            playerTransform = FindObjectOfType<PlayerMovement>().transform;
        }
    }

    private void Update()
    {
        position = playerTransform.position;
        position.z = -15;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
