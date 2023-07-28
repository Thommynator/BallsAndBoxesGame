using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{

    [SerializeField]    
    private TextMeshProUGUI _moneyText;

    public void UpdateMoneyText(int money)
    {
        _moneyText.text = money.ToString();
    }

}
