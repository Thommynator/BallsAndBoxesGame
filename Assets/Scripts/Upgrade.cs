using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Feedbacks;
using System;
using System.Globalization;

public class Upgrade : MonoBehaviour
{
    [Header("Upgrade Changes")]
    public string title;
    public string description;
    public Stats ballStats;
    public Stat statToUpgrade;
    public UpgradeFunctions upgradeFunctions;

    [Header("Linked Objects")]
    public TextMeshProUGUI titleText;
    public int currentLevel;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI effectText;
    public ProgressBar progressBar;

    [SerializeField]
    private MMF_Player _successfulPurchaseFeedback;

    [SerializeField]
    private MMF_Player _failedPurchaseFeedback;

    void Start()
    {
        UpdateText();
    }

    [ContextMenu("Upgrade")]
    public void TryToUpgradeStat()
    {
        if(upgradeFunctions.CanUpgrade(currentLevel))
        {
            var price = upgradeFunctions.GetPriceAtLevel(currentLevel);
            ballStats.IncreaseStat(statToUpgrade, upgradeFunctions.GetEffectAtLevel(currentLevel));
            MoneyManager.Instance.DecreaseMoneyBy((int)price);
            currentLevel++;
            UpdateText();
            EventManager.Instance.Upgrade();
            _successfulPurchaseFeedback.PlayFeedbacks();
        }
        else
        {
            _failedPurchaseFeedback.PlayFeedbacks();
            Debug.Log("Not enough money");
        }
       
    }

    private void UpdateText()
    {
        titleText.text = title;
        effectText.text = $"+ {FormatNumber(upgradeFunctions.GetEffectAtLevel(currentLevel))}";
        levelText.text = $"{currentLevel} / {upgradeFunctions.GetMaxLevel()}";
        progressBar.SetMaxValue((int)upgradeFunctions.GetPriceAtLevel(currentLevel));
    }

    private string FormatNumber(float number)
    {
        if(number % 1 == 0)
        {
            return $"{number}"; 
        } else
        {
           return number.ToString("N1", CultureInfo.CreateSpecificCulture("en-US")); // comma delimiter at 1000th & one decimal place
        }
    }

}

