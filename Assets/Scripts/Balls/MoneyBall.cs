using MoreMountains.Feedbacks;
using UnityEngine;

public class MoneyBall : Ball
{

    [SerializeField]
    private MMF_Player _moneyFeedback;

    protected override void ApplyDamageEffect(Collision2D collision)
    {
        base.PlayDamageSound();
        collision.gameObject.TryGetComponent<Box>(out var block);
        block?.DecreaseHealthBy(this.GetStats(), (int)_stats.TryToGetStat(Stat.HIT_DAMAGE));

        var extraMoney = (int)GetStats().TryToGetStat(Stat.MONEY_ON_COLLISION);
        _moneyFeedback.PlayFeedbacks(_moneyFeedback.transform.position, extraMoney);
        MoneyManager.Instance.IncreaseMoneyBy(extraMoney);
    }

}
