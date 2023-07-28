using UnityEngine;

public class ExplosiveBall : Ball
{

    protected override void ApplyDamageEffect(Collision2D collision)
    {
        base.ApplyDamageEffect(collision);

        var contactPoint = collision.contacts[0].point;
        var surroundingBlocks = Physics2D.OverlapCircleAll(contactPoint, _stats.TryToGetStat(Stat.EXPLOSION_RANGE), _dealsDamageTo);

        foreach (var blockCollider in surroundingBlocks)
        {
            if(blockCollider == collision.collider)
            {
                // this one was directly hit and doesn't receive an additional explosion damage
                continue;
            }

            blockCollider.TryGetComponent(out Block block);
            block?.DecreaseHealthBy((int)_stats.TryToGetStat(Stat.EXPLOSION_DAMAGE)); 
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stats.TryToGetStat(Stat.EXPLOSION_RANGE));
    }

}