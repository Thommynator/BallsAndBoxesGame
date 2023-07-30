using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    protected Rigidbody2D _body;

    [SerializeField]
    protected Stats _stats;

    [SerializeField]
    protected LayerMask _dealsDamageTo;

    protected Vector2 _lastVelocity;

    private int _enteredCollisionFrame = 0;

    public Stats GetStats()
    {
        return _stats;
    }

    protected virtual void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        transform.localScale = _stats.scale;
        GetComponentInChildren<SpriteRenderer>().sprite = _stats.sprite;
        GetComponentInChildren<SpriteRenderer>().color = _stats.color;

        _body.AddForce(new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized * _stats.TryToGetStat(Stat.SPEED), ForceMode2D.Impulse);
    
        StartCoroutine(SpeedUpToDesired());
    }

    protected virtual void Update()
    {
        // this is necessary, because I can't use _body.velocity in the collision event. It's always messed up.
        _lastVelocity = _body.velocity;
    }

    private IEnumerator SpeedUpToDesired()
    {
        while (true)
        {
            if (_body.velocity.magnitude < _stats.TryToGetStat(Stat.SPEED))
            {
                if (_body.velocity.magnitude < 0.1f)
                {
                    // random direction if too slow
                    _body.velocity = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized * _stats.TryToGetStat(Stat.SPEED);
                }
                else
                {
                    // speed up previous direction if too slow
                    _body.velocity = _body.velocity.normalized * _stats.TryToGetStat(Stat.SPEED);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        _enteredCollisionFrame = Time.frameCount;
        Bounce(collision);
        if (IsInLayerMask(collision.gameObject.layer, _dealsDamageTo))
        {
            ApplyDamageEffect(collision);
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (Time.frameCount - _enteredCollisionFrame > 10)
        {
            if (IsInLayerMask(collision.gameObject.layer, _dealsDamageTo))
            {
                ApplyDamageEffect(collision);
            }
        }
    }

    protected virtual void Bounce(Collision2D collision) {
        var normalVector = collision.contacts[0].normal;
        var newDirection = Vector3.Reflect(_lastVelocity, collision.contacts[0].normal).normalized;
        _body.velocity = newDirection * _stats.TryToGetStat(Stat.SPEED);
    }

    protected virtual void ApplyDamageEffect(Collision2D collision)
    {
        collision.gameObject.TryGetComponent<Box>(out var block);
        block?.DecreaseHealthBy((int)_stats.TryToGetStat(Stat.HIT_DAMAGE));
    }

    protected bool IsInLayerMask(int layer, LayerMask layerMask) {
        return layerMask == (layerMask | (1 << layer));
    }

}
