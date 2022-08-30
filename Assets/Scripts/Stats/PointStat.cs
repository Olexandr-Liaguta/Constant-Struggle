using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointStat
{
    [SerializeField]
    private int baseValue;

    private List<int> _modifiers = new();

    int _statModifier = 0;
    int _statModifierMultiplier = 50;
    public int currentValue { get; private set; }

    public void Initialize()
    {
        currentValue = GetMaxValue();
    }

    public int GetMaxValue()
    {
        int maxValue = baseValue;
        _modifiers.ForEach(x => maxValue += x);
        maxValue += _statModifier * _statModifierMultiplier;

        return maxValue;
    }

    public int Decrease(int by)
    {
        currentValue -= by;

        return currentValue;
    }

    public int Increase(int by)
    {
        currentValue += by;

        int maxValue = GetMaxValue();

        if(currentValue > maxValue)
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
