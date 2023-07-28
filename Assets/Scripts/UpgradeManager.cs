using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MoreMountains.Feedbacks;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private int _clickDamage;

    [SerializeField]
    private int _money;

    [SerializeField]
    private MoneyDisplay _moneyDisplay;

    public static UpgradeManager Instance;

    [SerializeField]
    private MMF_Player _moneyChangeFeedback;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        _moneyDisplay.UpdateMoneyText(_money);
    }

    public int GetClickDamange()
    {
        return _clickDamage;
    }
    public void IncreaseMoneyBy(int amount)
    {
        _money += amount;
        _moneyChangeFeedback.PlayFeedbacks();
        _moneyDisplay.UpdateMoneyText(_money);
    }

    public void DecreaseMoneyBy(int amount)
    {
        _money -= amount;
        _moneyChangeFeedback.PlayFeedbacks();
        _moneyDisplay.UpdateMoneyText(_money);
    }

    public int GetMoney()
    {
        return _money;
    }

    public void TryToBuyBall(Ball ball)
    {
        var price = ball.GetStats().TryToGetStat(Stat.PRICE);

        if (_money >= price)
        {
            DecreaseMoneyBy((int)price);
            BallSpawnManager.Instance.SpawnBall(ball);
            ball.GetStats().SetStat(Stat.PRICE, price * 2);
        }
    }

}