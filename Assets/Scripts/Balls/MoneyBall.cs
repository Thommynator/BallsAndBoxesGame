using UnityEngine;

public class MoneyBall : Ball
{

    protected override void ApplyDamageEffect(Collision2D collision)
    {
        base.PlayDamageSound();
        collision.gameObject.TryGetComponent<Box>(out var block);
        block?.DecreaseHealthBy((int)_stats.TryToGetStat(Stat.HIT_DAMAGE));

        var extraMoney = (int)GetStats().TryToGetStat(Stat.MONEY_ON_DEATH);
        Debug.Log($"+{extraMoney} extra money!");
        MoneyManager.Instance.IncreaseMoneyBy(extraMoney);
    }

}
