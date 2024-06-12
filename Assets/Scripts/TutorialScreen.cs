using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            GetComponent<CanvasGroup>().alpha = 0f;
        }
    }
}
