using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Image healthImage;

    [SerializeField]
    private TextMeshProUGUI healthText;

    private void Start()
    {
        PlayerStats.Instance.OnHealthChange += UpdateHealth;
    }

    public void UpdateHealth(object sender, PlayerStats.OnHealthChangeArgs e)
    {
        float healthPercent = e.health / e.maxHealth;

        healthImage.fillAmount = healthPercent;

        healthText.text = (int)e.health + " / " + (int)e.maxHealth;
    }
}
