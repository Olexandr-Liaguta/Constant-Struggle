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

    public void UpdateHealth(int current, int max)
    {
        float healthPercent = current / (float)max;

        healthImage.fillAmount = healthPercent;

        healthText.text = current + " / " + max;
    }
}
