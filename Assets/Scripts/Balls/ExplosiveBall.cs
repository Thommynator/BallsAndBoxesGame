using MoreMountains.Feedbacks;
using UnityEngine;

public class ExplosiveBall : Ball
{

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
            block?.DecreaseHealthBy((int)_stats.TryToGetStat(Stat.EXPLOSION_DAMAGE)); 
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stats.TryToGetStat(Stat.EXPLOSION_RANGE));
    }



}