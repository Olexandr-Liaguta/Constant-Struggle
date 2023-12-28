using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameManager : MonoBehaviour
{
    public static PauseGameManager Instance { get; private set; }

    public event EventHandler OnPauseGame;
    public event EventHandler OnResumeGame;

    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        OnPauseGame?.Invoke(this, EventArgs.Empty);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        OnResumeGame?.Invoke(this, EventArgs.Empty);
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }
}
