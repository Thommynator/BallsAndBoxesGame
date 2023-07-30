using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterBall : Ball
{
    [SerializeField]
    private Ball _miniBall;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (!IsInLayerMask(collision.gameObject.layer, _dealsDamageTo))
        {
            SpawnScatterBalls();
        }
    }

    private void SpawnScatterBalls()
    {
        for (int i = 0; i < _stats.TryToGetStat(Stat.NUMBER_OF_MINI_SCATTER_BALLS); i++)
        {
            var miniBall = Instantiate(_miniBall, transform.position, Quaternion.identity);
            miniBall.transform.SetParent(transform.parent);
            miniBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * _stats.TryToGetStat(Stat.SPEED), ForceMode2D.Impulse);
        }
    }
}