using MoreMountains.Tools;
using System;
using UnityEngine;

[Serializable]
public class UpgradeFunction
{
    [Tooltip("Describes how the value increases/decreases with each level.")]
    [SerializeField]
    private AnimationCurve _function;

    [SerializeField]
    private RoundType _roundType;

    [Tooltip("The value that represents the 0 on the y-axis of the animation curve.")]
    [SerializeField]
    private float _mapToZero;

    [Tooltip("The value that represents the 1 on the y-axis of the animation curve.")]
    [SerializeField]
    private float _mapToOne;

    public float GetValueAtLevel(int level, int maxLevel)
    {
        _function.postWrapMode = WrapMode.ClampForever;

        var xValue = ((float)level) / maxLevel;
        var value = MMMaths.Remap(_function.Evaluate(xValue), 0f, 1f, _mapToZero, _mapToOne);

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
