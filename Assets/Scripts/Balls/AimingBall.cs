using UnityEngine;
using System.Linq;

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
        var allActiveBoxes = LevelManager.Instance.GetCurrentBoxes().FindAll(box => box.GetBoxStatus() == BoxStatus.ALIVE).ToList();
        Box nearestBox = null;
        float shortestDistanceToTargetSquared = float.MaxValue;

        foreach (var box in allActiveBoxes)
        {
            var distanceToTargetSquared = (box.transform.position - transform.position).sqrMagnitude;
            if (distanceToTargetSquared < shortestDistanceToTargetSquared)
            {
                nearestBox = box;
                shortestDistanceToTargetSquared = distanceToTargetSquared;
            }
        }
        return nearestBox;
    }

}
