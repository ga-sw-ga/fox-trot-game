using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public GameObject UICanvas;
    private bool isUIShown = false;
    private bool hasGameStarted = false;

    private void Awake()
    {
        instance = this;
        
        PauseGame();
    }

    private void Update()
    {
        if (Input.anyKeyDown && !hasGameStarted)
        {
            UnPauseGame();
            hasGameStarted = true;
        }
        if (Input.GetMouseButtonDown(2))
        {
            CanvasGroup canvasGroup = UICanvas.GetComponent<CanvasGroup>();
            if (!isUIShown)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
                PauseGame();
            }
            else
            {
                canvasGroup.alpha = 0f;
                canvasGroup.blocksRaycasts = false;
                UnPauseGame();
            }
            isUIShown = !isUIShown;
        }
    }

    public void ResetGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
