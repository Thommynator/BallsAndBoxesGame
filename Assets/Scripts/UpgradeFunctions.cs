using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeFunctions", menuName = "ScriptableObjects/UpgradeFunctions", order = 1)]
public class UpgradeFunctions : ScriptableObject
{
    [SerializeField]
    private int _maxLevel;

    [SerializeField]
    private UpgradeFunction _priceFunction;

    [SerializeField]
    private UpgradeFunction _effectFunction;

    public bool CanUpgrade(int desiredLevel)
    {
        return desiredLevel < _maxLevel && _priceFunction.GetValueAtLevel(desiredLevel, _maxLevel) <= MoneyManager.Instance.GetMoney();
    }
    public float GetPriceAtLevel(int level)
    {
        return _priceFunction.GetValueAtLevel(level, _maxLevel);
    }
    public float GetEffectAtLevel(int level)
    {
        return _effectFunction.GetValueAtLevel(level, _maxLevel);
    }
    public int GetMaxLevel()
    {
        return _maxLevel;
    }
}
