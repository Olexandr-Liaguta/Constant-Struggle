using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipStatUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleTextComponent, valueTextComponent;

    public void SetStatText(string title, ModifierValue value, bool isAdditional)
    {
        titleTextComponent.text = title;

        if (value.value == 0)
        {
            if (value.min == value.max)
            {
                valueTextComponent.text = value.max.ToString();
            } else
            {
                valueTextComponent.text = value.min.ToString() + " - " + value.max.ToString();
            }
        }
        else
        {
            valueTextComponent.text = value.value.ToString();
        }

        if (isAdditional)
        {
            titleTextComponent.color = Color.blue;
            valueTextComponent.color = Color.blue;
        }
    }
}
