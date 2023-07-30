using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AimingBall: Ball
{

    protected override void Bounce(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            AimTowardsNearestBlock(collision);
        } else
        {
            base.Bounce(collision);
        }
    } 

    private void AimTowardsNearestBlock(Collision2D collision)
    {
        var nearestBlock = FindNearestBlock();
        if (nearestBlock == null)
        {
            base.Bounce(collision);
            return;
        } 

        var direction = (nearestBlock.transform.position - transform.position).normalized;
        _body.velocity = direction * _stats.TryToGetStat(Stat.SPEED);
    }

    private Box FindNearestBlock()
    {
        var allActiveBlocks = FindObjectsOfType<Box>();
        Box nearestBlock = null;
        float shortestDistanceToTargetSquared = float.MaxValue;

        foreach (var block in allActiveBlocks)
        {
            var distanceToTargetSquared = (block.transform.position - transform.position).sqrMagnitude;
            if (distanceToTargetSquared < shortestDistanceToTargetSquared)
            {
                nearestBlock = block;
                shortestDistanceToTargetSquared = distanceToTargetSquared;
            }
        }
        return nearestBlock;
    }

}
