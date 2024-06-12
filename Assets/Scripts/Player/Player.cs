using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float MAX_MOVEMENT_VELOCITY = 8f,
                        JUMP_FORCE = 12f,
                        MOVE_FORCE = 50f,
                        STOP_FORCE = -50f;
    
    private Rigidbody rb;
    private Animator animator;

    [HideInInspector] public bool canJump;
    public bool isExtendJumping;

    private Vector3 faceDir = Vector3.left;
    private float horizontalInput = 0f;
    private Vector3 horizontalVelocity = Vector3.zero;
    private float detectionRadius = 1f;
    private Collectible closestCollectible, grabbedCollectible;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
    
    private void Update()
    {
        // DETECTING CLOSEST COLLECTIBLE
        FindClosestCollectibleInRadius();
        
        // JUMPING
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
            isExtendJumping = true;
            canJump = false;
            AudioManager.instance.PlaySFX(AudioManager.instance.jump);
        }

        if (Input.GetKeyUp(KeyCode.Space) || rb.velocity.y < -0.1f)
        {
            isExtendJumping = false;
        }
        
        // ORIENTATION
        if (horizontalInput > 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            faceDir = Vector3.right;
        }
        else if (horizontalInput < 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            faceDir = Vector3.left;
        }
        
        // GRAB MECHANIC
        if (Input.GetKeyDown(KeyCode.LeftControl) && closestCollectible != null)
        {
            if (grabbedCollectible != null)
            {
                grabbedCollectible.GetUnGrabbed(closestCollectible.transform.position);
                grabbedCollectible = null;
            }
            closestCollectible.GetGrabbed(transform, transform.position + faceDir.normalized * 0.75f);
            grabbedCollectible = closestCollectible;
            AudioManager.instance.PlaySFX(AudioManager.instance.pickup);
        }
        
        // THROW MECHANIC
        if (Input.GetKeyDown(KeyCode.LeftAlt) && grabbedCollectible != null && grabbedCollectible.GetComponent<Sword>() != null)
        {
            grabbedCollectible.GetComponent<Sword>().ThrowSword(faceDir);
            grabbedCollectible = null;
            // Debug.Log(AudioManager.instance.sword == null);
            AudioManager.instance.PlaySFX(AudioManager.instance.sword);
        }
        
        animator.SetFloat("WalkAnimationSpeed", math.abs(horizontalVelocity.x / MAX_MOVEMENT_VELOCITY));
        
    }

    private void FixedUpdate()
    {
        // L&R MOVEMENT
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 horizontalMoveVector = new Vector3(horizontalInput * MAX_MOVEMENT_VELOCITY, 0f, 0f);
        PhysicsUtils.AddForceToReachVelocity(rb, horizontalMoveVector, MOVE_FORCE, ForceMode.Acceleration);
        
        // STOP FORCE
        horizontalVelocity = Vector3.right * rb.velocity.x;
        if (math.abs(horizontalInput - 0f) < 0.01f && horizontalVelocity.magnitude > 0f)
        {
            rb.AddForce(horizontalVelocity * STOP_FORCE, ForceMode.Acceleration);
        }

        // CHOOSING CORRECT ANIMATION
        if (horizontalVelocity.magnitude > 0.01f)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        
        // JUMP EXTEND
        if (isExtendJumping)
        {
            rb.AddForce(Vector3.up * 16f, ForceMode.Acceleration);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("YOU DEAD DEAD!");
            GameObject.Find("CanvasLose").transform.GetChild(1).GetComponent<CanvasGroup>().alpha = 1;
            AudioManager.instance.musicSource.Stop();
            Invoke("ResetGame", 3f);
        }
    }

    private void Jump()
    {
        rb.AddForce(JUMP_FORCE * Vector3.up, ForceMode.VelocityChange);
    }
    
    private void FindClosestCollectibleInRadius()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        float closestDistance = Mathf.Infinity;
        closestCollectible = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Collectible"))
            {
                float distanceToCollectible = Vector3.Distance(transform.position, hitCollider.transform.position);
                Collectible collectible = hitCollider.gameObject.GetComponent<Collectible>();
                if (distanceToCollectible < closestDistance && grabbedCollectible != collectible && collectible.isGrabbable)
                {
                    closestDistance = distanceToCollectible;
                    closestCollectible = collectible;
                }
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    void ResetGame()
    {
        GameManager.instance.ResetGame();
    }
}
