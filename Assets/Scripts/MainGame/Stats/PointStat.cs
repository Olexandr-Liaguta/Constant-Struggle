using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointStat
{
    [SerializeField]
    private float baseValue;

    private List<int> modifiers = new();

    public float currentValue { get; private set; }

    public void SetCurrentValueOnMax()
    {
        currentValue = GetMaxValue();
    }

    public float GetMaxValue()
    {
        float maxValue = baseValue;
        modifiers.ForEach(x => maxValue += x);
        return maxValue;
    }

    public float GetAmount01()
    {
        return currentValue / GetMaxValue();
    }

    public float Decrease(float by)
    {
        currentValue -= by;

        return currentValue;
    }

    public float Increase(float by)
    {
        currentValue += by;

        float maxValue = GetMaxValue();

        if (currentValue > maxValue)
        {
            currentValue = maxValue;
        }

        return currentValue;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
            currentValue += modifier;
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
            currentValue -= modifier;

            if (currentValue <= 0)
            {
                currentValue = 1;
            }
        }
    }

    public void RemoveAllModifiers()
    {
        modifiers.Clear();
    }

}
