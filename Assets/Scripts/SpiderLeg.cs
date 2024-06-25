using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeg : MonoBehaviour
{
    private const float MOVE_SPEED = 0.5f,
                        MOVE_RANGE = 5f;

    private RectTransform rectTransform;
    
    private float timer = 0f;
    private float defaultZRotation;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultZRotation = rectTransform.eulerAngles.z;
    }

    private void Update()
    {
        float angleOffset = MOVE_RANGE * 0.5f * Mathf.Sin(Mathf.PI * 2f * timer * MOVE_SPEED);
        rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, rectTransform.eulerAngles.y,
            defaultZRotation + angleOffset);
        timer += Time.deltaTime;
    }
}
