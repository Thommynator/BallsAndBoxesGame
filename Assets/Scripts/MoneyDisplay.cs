using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class MoneyDisplay : MonoBehaviour
{

    [SerializeField]    
    private TextMeshProUGUI _moneyText;

    public void UpdateMoneyText(int money)
    {
        _moneyText.text = money.ToString("N0", CultureInfo.CreateSpecificCulture("en-US"));
    }

}
