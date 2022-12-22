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

    public void UpdateHealth(float current, float max)
    {
        float healthPercent = current / max;

        healthImage.fillAmount = healthPercent;

        healthText.text = (int)current + " / " + (int)max;
    }
}
