using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private GameObject mainComponent;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private Image healthComponent;

    private void Start()
    {
        PlayerCombat.Instance.OnFocusEnemy += Show;
        PlayerCombat.Instance.OnUnfocusEnemy += Hide;

        mainComponent.SetActive(false);
    }

    public void Show(object sender, PlayerCombat.OnFocusEnemyArgs args)
    {
        textComponent.text = args.enemyStats.enemyName;

        UpdateHealth(args.enemyStats);

        mainComponent.SetActive(true);
    }

    public void UpdateHealth(EnemyStats enemyStats)
    {
        healthComponent.fillAmount = enemyStats.health.GetAmount01();
    }

    public void Hide(object sender, EventArgs args)
    {
        mainComponent.SetActive(false);
    }
}
