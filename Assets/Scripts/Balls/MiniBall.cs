using UnityEngine;

public class MiniBall: Ball
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision);
        if (IsInLayerMask(collision.gameObject.layer, _dealsDamageTo))
        {
            ApplyDamageEffect(collision);
            Destroy(gameObject);
        }
    }

    protected override void ApplyDamageEffect(Collision2D collision)
    {
        base.PlayDamageSound();
        collision.gameObject.TryGetComponent<Box>(out var block);
        block?.DecreaseHealthBy(this.GetStats(), Mathf.FloorToInt(_stats.TryToGetStat(Stat.HIT_DAMAGE)));
    }

}
