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

    public void UpdateMana(int current, int max)
    {
        float manaPercent = current / (float)max;

        manaImage.fillAmount = manaPercent;

        manaText.text = current + " / " + max;
    }
}
