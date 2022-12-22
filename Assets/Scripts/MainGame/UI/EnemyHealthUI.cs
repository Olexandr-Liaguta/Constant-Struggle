using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField]
    GameObject mainComponent;

    [SerializeField]
    TextMeshProUGUI textComponent;

    [SerializeField]
    Image healthComponent;

    private void Start()
    {
        mainComponent.SetActive(false);
    }

    public void Show(EnemyStats enemyStats)
    {
        textComponent.text = enemyStats.enemyName;

        UpdateHealth(enemyStats);

        mainComponent.SetActive(true);
    }

    public void UpdateHealth(EnemyStats enemyStats)
    {
        healthComponent.fillAmount = enemyStats.health.GetAmount01();
    }

    public void Hide()
    {
        mainComponent.SetActive(false);
    }
}
