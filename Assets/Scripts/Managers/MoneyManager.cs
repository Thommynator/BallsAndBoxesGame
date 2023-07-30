using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    private Stats _mouseClick;

    [SerializeField]
    private int _money;

    [SerializeField]
    private MoneyDisplay _moneyDisplay;

    public static MoneyManager Instance;

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
        return (int)_mouseClick.TryToGetStat(Stat.HIT_DAMAGE);
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

    public bool TryToBuyBall(Ball ball)
    {
        var price = ball.GetStats().TryToGetStat(Stat.PRICE);

        if (_money >= price)
        {
            DecreaseMoneyBy((int)price);
            BallSpawnManager.Instance.SpawnBall(ball);
            ball.GetStats().SetStat(Stat.PRICE, price * 2);
            return true;
        }
        return false;
    }

}