using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Collectible
{
    private const float THROW_SPEED = 11f;
    
    private Rigidbody rb;

    [HideInInspector] public bool isThrown = false;
    private Vector3 throwDir;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // private void Update()
    // {
    //     if (isThrown)
    //     {
    //         transform.eulerAngles += Vector3.right * (360f * Time.deltaTime);
    //     }
    // }

    private void FixedUpdate()
    {
        if (isThrown)
        {
            rb.MovePosition(throwDir * (THROW_SPEED * Time.fixedDeltaTime) + transform.position);
            // rb.MoveRotation(Quaternion.Euler(transform.eulerAngles + Vector3.right * (360f * Time.fixedDeltaTime)));
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, 1080 * Time.fixedDeltaTime * throwDir.x, 0));
        }
    }

    public void ThrowSword(Vector3 direction)
    {
        transform.parent = GameObject.Find("Collectibles").transform;
        throwDir = direction.normalized;
        isThrown = true;
    }
}
