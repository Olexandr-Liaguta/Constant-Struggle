using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndLevelUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerText;

    private float countdownTimer;
    private bool isCountdownActive = false;

    private void Start()
    {
        SceneProgressManager.Instance.OnAllEnemiesDie += SceneProgressManager_OnAllEnemiesDie;
        Hide();
    }

    private void SceneProgressManager_OnAllEnemiesDie(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Update()
    {
        if (isCountdownActive && countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            timerText.text = ((int)countdownTimer).ToString();
        }
    }

    private void Show() 
    {
        countdownTimer = SceneProgressManager.Instance.TIME_AFTER_COMPLETE_LEVEL;
        isCountdownActive = true;
        gameObject.SetActive(true);
    }


    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
