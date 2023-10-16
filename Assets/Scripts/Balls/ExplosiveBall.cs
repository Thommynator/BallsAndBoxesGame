using MoreMountains.Feedbacks;
using UnityEngine;

public class ExplosiveBall : Ball
{

    [SerializeField]
    protected LayerMask _affectedByExplosionForce;

    [SerializeField]
    private ParticleSystem _explosionParticles;

    [SerializeField]   
    private MMF_Player _explosionFeedback;

    protected override void Start()
    {
        base.Start();
        UpgradeExplosionRangeVisuals();
        EventManager.Instance.onUpgrade += UpgradeExplosionRangeVisuals;
    }

    private void OnDestroy()
    {
        EventManager.Instance.onUpgrade -= UpgradeExplosionRangeVisuals;
    }

    protected override void Update()
    {
        base.Update();

    }

    protected override void ApplyDamageEffect(Collision2D collision)
    {
        var contactPoint = collision.contacts[0].point;
        var surroundingBlocks = Physics2D.OverlapCircleAll(contactPoint, _stats.TryToGetStat(Stat.EXPLOSION_RANGE), _dealsDamageTo);
        PlayExplosionParticleEffect(contactPoint);
        PlayDamageSound();
        _explosionFeedback.PlayFeedbacks();

        foreach (var blockCollider in surroundingBlocks)
        {
            blockCollider.TryGetComponent(out Box block);
            block?.DecreaseHealthBy(this.GetStats(), (int)_stats.TryToGetStat(Stat.EXPLOSION_DAMAGE)); 
        }

        PushOtherObjectsAround(contactPoint);
    }

    private void PushOtherObjectsAround(Vector3 originPosition)
    {
        var surroundingObjects = Physics2D.OverlapCircleAll(originPosition, _stats.TryToGetStat(Stat.EXPLOSION_RANGE), _affectedByExplosionForce);

        foreach(var surroundingObject in surroundingObjects)
        {
            surroundingObject.TryGetComponent(out Rigidbody2D rb);
            if (rb != null) {
                AddExplosionForce(rb, _stats.TryToGetStat(Stat.EXPLOSION_DAMAGE), originPosition, _stats.TryToGetStat(Stat.EXPLOSION_RANGE));
            }
        }
    }

    private void PlayExplosionParticleEffect(Vector3 position)
    {
        _explosionParticles.transform.position = position;
        _explosionParticles.Play();
    }

    private void UpgradeExplosionRangeVisuals()
    {
        var main = _explosionParticles.main;
        var duration = _stats.TryToGetStat(Stat.EXPLOSION_RANGE) / main.startSpeed.constant;
        main.duration = duration;
        main.startLifetime = duration;
    }

    private void AddExplosionForce(Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Impulse)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        // Normalize without computing magnitude again
        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else
        {
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        var explosionDistanceNorm = explosionDistance / explosionRadius;
        var force = Mathf.Lerp(explosionForce, 0, explosionDistanceNorm);
        var forceMultiplier = 6.0f;

        rb.AddForce(force * forceMultiplier * explosionDir.normalized, mode);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stats.TryToGetStat(Stat.EXPLOSION_RANGE));
    }



}