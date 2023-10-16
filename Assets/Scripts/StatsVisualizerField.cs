using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class StatsVisualizerField : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _keyText;

    [SerializeField]
    private TextMeshProUGUI _valueText;

    private Stats _stats;
    private Stat _stat;

    public StatsVisualizerField Initialize(Stats stats, Stat stat)
    {
        _stats = stats;
        _stat = stat;
        return this;
    }

    private void Update()
    {
        _keyText.text = _stat.GetStringValue();
        _valueText.text = FormatNumber(_stats.TryToGetStat(_stat));
    }

    private string FormatNumber(float number)
    {
        if (number % 1 == 0)
        {
            return $"{number}";
        }
        else
        {
            return number.ToString("N1", CultureInfo.CreateSpecificCulture("en-US")); // comma delimiter at 1000th & one decimal place
        }
    }

}
