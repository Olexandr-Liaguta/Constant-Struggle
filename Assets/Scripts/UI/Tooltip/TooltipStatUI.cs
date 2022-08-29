using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipStatUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleTextComponent, valueTextComponent;

    public void SetStatText(string title, string value, bool isAdditional)
    {
        titleTextComponent.text = title;
        valueTextComponent.text = value;

        if(isAdditional)
        {
            titleTextComponent.color = Color.blue;
            valueTextComponent.color = Color.blue;
        }
    }
}
