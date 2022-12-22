using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBarUI : MonoBehaviour
{
    [SerializeField]
    private Image manaImage;

    [SerializeField]
    private TextMeshProUGUI manaText;

    public void UpdateMana(float current, float max)
    {
        float manaPercent = current / max;

        manaImage.fillAmount = manaPercent;

        manaText.text = (int)current + " / " + (int)max;
    }
}
