using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton, mapButton, exitButton;
    [SerializeField] private GameObject pauseUIContainer;

    private void Start()
    {
        PauseGameManager.Instance.OnPauseGame += PauseGameManager_OnPauseGame;
        PauseGameManager.Instance.OnResumeGame += PauseGameManager_OnResumeGame;

        resumeButton.onClick.AddListener(() =>
        {
            PauseGameManager.Instance.ResumeGame();
        });

        mapButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.Map);
        });

        exitButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.MainMenu);
        });

        Hide();
    }

    private void PauseGameManager_OnResumeGame(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void PauseGameManager_OnPauseGame(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        pauseUIContainer.SetActive(true);
    }

    private void Hide()
    {
        pauseUIContainer.SetActive(false);
    }
}
