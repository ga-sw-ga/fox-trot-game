using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLegs : MonoBehaviour
{
    private const float GROUND_HEIGHT = -1.5f;
    
    private RectTransform rectTransform;
    private Transform enemies;
    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        enemies = GameObject.FindWithTag("Enemies").transform;
    }

    void Update()
    {
        if (enemies.childCount > 0)
        {
            rectTransform.sizeDelta = Vector2.left * (140f * Mathf.Max(0f, 6f + GROUND_HEIGHT - enemies.GetChild(0).position.y));
        }
    }
}
