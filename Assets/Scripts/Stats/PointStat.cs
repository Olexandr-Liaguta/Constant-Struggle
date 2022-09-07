using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointStat
{
    [SerializeField]
    private float baseValue;

    private List<int> _modifiers = new();

    int _statModifier = 0;
    int _statModifierMultiplier = 50;
    public float currentValue { get; private set; }

    public void Initialize()
    {
        currentValue = GetMaxValue();
    }

    public float GetMaxValue()
    {
        float maxValue = baseValue;
        _modifiers.ForEach(x => maxValue += x);
        maxValue += _statModifier * _statModifierMultiplier;

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

    public void SetStatModifier(int statModifier)
    {
        _statModifier = statModifier;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            _modifiers.Add(modifier);
            currentValue += modifier;
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            _modifiers.Remove(modifier);
            currentValue -= modifier;

            if (currentValue <= 0)
            {
                currentValue = 1;
            }
        }
    }

}
