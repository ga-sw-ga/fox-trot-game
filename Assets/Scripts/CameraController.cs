using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float SMOOTH_TIME = 0.03f;
    
    public Transform target;

    private Vector3 velocity;
    
    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 dampedPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, SMOOTH_TIME);
            transform.position = new Vector3(dampedPos.x, dampedPos.y, transform.position.z);
        }
    }
}
