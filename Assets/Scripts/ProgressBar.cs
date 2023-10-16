using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Tools;
using static UnityEngine.Rendering.DebugUI;
using System.Globalization;

public class ProgressBar : MonoBehaviour
{

    [SerializeField]
    private GameObject _foregroundBar;

    [SerializeField]
    private TextMeshProUGUI _text;

    private int _maxValue;

    public void SetMaxValue(int maxValue)
    {
        _maxValue = maxValue;
    }

    void Update()
    {
        var money = MoneyManager.Instance.GetMoney();
        var scale = Mathf.Min(MMMaths.Remap(money, 0, _maxValue, 0, 1), 1);
        _foregroundBar.transform.localScale = new Vector3(scale, 1, 1);

        _text.text = _maxValue.ToString("N0", CultureInfo.CreateSpecificCulture("en-US"));
    }
}
