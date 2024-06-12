using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlToy : MonoBehaviour
{
    public static GirlToy instance;
    
    private const float OPERATE_DEFAULT_TIME = 3f,
                        OPERATION_SPEED = 1f;
    
    private Rigidbody rb;
    private Animator animator;

    private float detectionRadius = 1f;
    private float operationTimer = 0f;

    private void Awake()
    {
        instance = this;
        
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Coin coin = GetClosestCoinInRadius();
        if (coin != null)
        {
            coin.isGrabbable = false;
            Operate(OPERATE_DEFAULT_TIME);
            Destroy(coin.gameObject);
        }
        
        // MOVING THE GIRL FORWARD
        if (operationTimer > 0f)
        {
            operationTimer -= Time.deltaTime;
            operationTimer = Mathf.Max(operationTimer, 0f);
            if (operationTimer <= 0)
            {
                animator.SetBool("IsWalking", false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (operationTimer > 0f)
        {
            rb.MovePosition(Vector3.right * (OPERATION_SPEED * Time.deltaTime) + transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("YOU FAILED!");
            GameObject.Find("CanvasLose").transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1;
            AudioManager.instance.musicSource.Stop();
            Invoke("ResetGame", 3f);
        }
    }

    private Coin GetClosestCoinInRadius()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<Coin>() != null)
            {
                Coin coin = hitCollider.gameObject.GetComponent<Coin>();
                return coin;
            }
        }

        return null;
    }

    private void Operate(float time)
    {
        rb.velocity = Vector3.right * OPERATION_SPEED;
        operationTimer += time;
        AudioManager.instance.PlaySFX(AudioManager.instance.coin);
        AudioManager.instance.PlaySFX(AudioManager.instance.dollmove);
        animator.SetBool("IsWalking", true);
    }

    private void ResetGame()
    {
        GameManager.instance.ResetGame();
    }
}
