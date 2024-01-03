using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InventoryStatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI baseText, currentText;
    [SerializeField] private GameObject baseTextGO;

    public void SetStats(string currentValue, string baseValue)
    {
        baseTextGO.SetActive(true);
        baseText.text = baseValue;
        currentText.text = currentValue;
    }

    public void SetStats(string currentValue )
    {
        baseTextGO.SetActive(false);
        baseText.text = "";
        currentText.text = currentValue;
    }
}
