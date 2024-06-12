using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float GROUND_HEIGHT = -1.5f,
                        DESCEND_SPEED = 0.25f,
                        SIDE_SPEED = 2f;
    
    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (rb.position.y > GROUND_HEIGHT)
        {
            rb.MovePosition(rb.position + DESCEND_SPEED * Time.fixedDeltaTime * Vector3.down);
        }
        else
        {
            if (transform.GetChild(0).childCount > 1)
            {
                Destroy(transform.GetChild(0).GetChild(1).gameObject);
            }
            Vector3 moveDir = GirlToy.instance.transform.position - transform.position;
            moveDir.z = moveDir.y = 0f;
            moveDir = moveDir.normalized;
            rb.MovePosition(rb.position + SIDE_SPEED * Time.fixedDeltaTime * moveDir);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Sword sword = other.GetComponent<Sword>();
        if (sword != null && sword.isThrown)
        {
            Destroy(sword.gameObject);
            Destroy(gameObject);
        }
    }
}
