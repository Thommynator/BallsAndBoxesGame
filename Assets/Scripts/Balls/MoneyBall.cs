using UnityEngine;

public class MoneyBall : Ball
{

    protected override void ApplyDamageEffect(Collision2D collision)
    {
        collision.gameObject.TryGetComponent<Box>(out var block);
        var status = block?.DecreaseHealthBy((int)_stats.TryToGetStat(Stat.HIT_DAMAGE));

        if(status == BlockStatus.DEAD)
        {
            var extraMoney = (int)GetStats().TryToGetStat(Stat.MONEY_ON_DEATH);
            Debug.Log($"+{extraMoney} extra money!");
            MoneyManager.Instance.IncreaseMoneyBy(extraMoney);
        }
    }

}
