using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [HideInInspector] public bool isGrabbed = false;
    [HideInInspector] public bool isGrabbable = true;

    private bool hasAppeared = true;
    private float timer = 0f;

    private void Update()
    {
        if (timer > 0f && hasAppeared)
        {
            transform.Translate(Vector3.up * ((1.5f / 1f) * Time.deltaTime));
            timer -= Time.deltaTime;
        }
        else if (timer <= 0f && hasAppeared)
        {
            isGrabbable = true;
        }
    }

    public void GetGrabbed(Transform grabber, Vector3 newPos)
    {
        if (!isGrabbed && isGrabbable)
        {
            transform.parent = grabber;
            transform.position = newPos;
            isGrabbed = true;
        }
    }

    public void GetUnGrabbed(Vector3 newPos)
    {
        if (isGrabbed)
        {
            transform.parent = GameObject.Find("Collectibles").transform;
            transform.position = newPos;
            isGrabbed = false;
        }
    }

    public void Appear()
    {
        hasAppeared = true;
    }
}
