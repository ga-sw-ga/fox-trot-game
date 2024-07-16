using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetectorTrigger : MonoBehaviour
{
    private Player player;
    
    private void Awake()
    {
        player = transform.parent.parent.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag())
        // {
        //    player.canJump = true;
        // }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
            player.canJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            player.canJump = false;
        }
    }
}
