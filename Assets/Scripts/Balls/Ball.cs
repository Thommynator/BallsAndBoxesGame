using MoreMountains.Feedbacks;
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

    private MMF_Player _feedbackPlayer = null;

    public Stats GetStats()
    {
        return _stats;
    }

    protected virtual void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        transform.localScale = _stats.scale;
        GetComponentInChildren<SpriteRenderer>().sprite = _stats.sprite;

        InitializeSoundFeedback();
        //_body.AddForce(new Vector2(1,0).normalized * _stats.TryToGetStat(Stat.SPEED), ForceMode2D.Impulse);
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

        var noise = ReflectionNoise(newDirection, 0.5f);
        _body.velocity = (newDirection + noise).normalized * _stats.TryToGetStat(Stat.SPEED);

    }

    /**
     * If the reflection direction is too close to the x or y axis, add some noise to it
     * to prevent that the ball is stuck in a loop.
     */
    private Vector3 ReflectionNoise(Vector3 reflectionDirection, float noiseStrength)
    {
        reflectionDirection = reflectionDirection.normalized;
        float dotX = Mathf.Abs(Vector3.Dot(reflectionDirection, Vector3.right));
        float dotY = Mathf.Abs(Vector3.Dot(reflectionDirection, Vector3.up));
        float similarityThreshold = 0.95f; // 1 means it's aligned with the axis, 0 is perpendicular

        if (dotX > similarityThreshold || dotY > similarityThreshold)
        {
            return new Vector3(1, 1, 0) * Random.Range(-noiseStrength, noiseStrength);
        }

        return Vector3.zero;
    }

    protected virtual void ApplyDamageEffect(Collision2D collision)
    {
        PlayDamageSound();
        collision.gameObject.TryGetComponent<Box>(out var block);
        block?.DecreaseHealthBy(this.GetStats(), (int)_stats.TryToGetStat(Stat.HIT_DAMAGE));
    }

    protected virtual void PlayDamageSound()
    {
        _feedbackPlayer?.PlayFeedbacks();
    }

    protected bool IsInLayerMask(int layer, LayerMask layerMask) {
        return layerMask == (layerMask | (1 << layer));
    }

    private void InitializeSoundFeedback()
    {
        if (_stats.hitSounds.Count > 0)
        {
            _feedbackPlayer = gameObject.AddComponent<MMF_Player>();
            MMF_MMSoundManagerSound soundFeedback = new MMF_MMSoundManagerSound();
            soundFeedback.RandomSfx = _stats.hitSounds.ToArray();
            _feedbackPlayer.AddFeedback(soundFeedback);
            _feedbackPlayer.Initialization();
        }
    }

}
