using System;
using UnityEngine;


[Serializable]
public class UpgradeProperty
{
    public float _baseValue;
    [Tooltip("Describes how the value increases with each level. base + level * factor")]
    public float _incrementFactor;
    public int _level;
    public RoundType _roundType;

    public float GetCurrentValue()
    {
       return GetValueAtLevel(_level);
    }

    public float GetNextValue()
    {
        return GetValueAtLevel(_level + 1);
    }

    public float GetValueAtLevel(int level)
    {
        var value = _baseValue + _level * _incrementFactor;
       
        switch (_roundType)
        {
            case RoundType.Floor:
                return Mathf.Floor(value);
            case RoundType.Ceil:
                return Mathf.Ceil(value);
            case RoundType.Round:
                return Mathf.Round(value);
            case RoundType.None:
                return value;
            default:
                return value;
        }
    }
    
}

[Serializable]
public enum RoundType
{
    Round,
    Floor,
    Ceil,
    None
}
