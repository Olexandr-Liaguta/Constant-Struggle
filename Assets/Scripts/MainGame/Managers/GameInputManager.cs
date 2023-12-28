using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGameManager.Instance.IsGamePaused())
            {
                PauseGameManager.Instance.ResumeGame();
            }
            else
            {
                PauseGameManager.Instance.PauseGame();
            }
        }
    }
}
