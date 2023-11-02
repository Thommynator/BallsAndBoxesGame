using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

public class Box : MonoBehaviour
{

    [SerializeField]
    private int _startHealth;

    private int _health;

    [SerializeField]
    private TextMeshProUGUI _healthText;

    [SerializeField]    
    private List<Color> _colors;

    [SerializeField]
    private BoxSO _boxSO;

    [SerializeField]
    private MMF_Player _clickOnBoxFeedback;

    [SerializeField]
    private MMF_Player _openBoxFeedback;

    [SerializeField]
    private MMF_Player _closeBoxFeedback;

    [SerializeField]
    private MMF_Player _hitBoxFeedback;

    [SerializeField]
    private MMF_Player _floatingMoneyFeedback;

    [SerializeField]
    private ParticleSystem _hitParticles;


    private SpriteRenderer _spriteRenderer;

    private BoxStatus _boxStatus;

    private GameObject _wrapper;

    private Collider2D _collider;



    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = _boxSO.GetRandomSprite();
        _wrapper = transform.Find("Wrapper").gameObject;
        _collider = GetComponent<Collider2D>();
    }
    void Start()
    {
    }


    private void SetStartHealth(int startHealth)
    {
        _startHealth = startHealth;
        _health = _startHealth;
        UpdateVisuals();
    }

    public BoxStatus GetBoxStatus()
    {
        return _boxStatus;
    }

    public void Hide()
    {
        _wrapper.SetActive(false);
        _collider.enabled = false;
    }

    public void Activate(int startHealth)
    {
        SetStartHealth(startHealth);
        _openBoxFeedback.PlayFeedbacks();
        _boxStatus = BoxStatus.ALIVE;
    }

    public BoxStatus DecreaseHealthBy(Stats damageDealer, int damage, int moneyMultiplier = 1)
    {
        var realDamage = Mathf.Min(damage, _health);
        _floatingMoneyFeedback.PlayFeedbacks(_floatingMoneyFeedback.transform.position, realDamage);
        _health -= realDamage;

        MoneyManager.Instance.IncreaseMoneyBy(realDamage * moneyMultiplier);
        damageDealer.IncreaseStat(Stat.TOTAL_DAMAGE, realDamage);

        if (IsDead())
        {
            _closeBoxFeedback.PlayFeedbacks();
            return BoxStatus.DEAD;
        } else
        {
            UpdateVisuals();
            return BoxStatus.ALIVE;
        }
    }

    private void UpdateVisuals()
    {
        _healthText.text = _health.ToString();
        ChangeBlockColor();
    }

    private void OnMouseDown()
    {
        _clickOnBoxFeedback.PlayFeedbacks();
        _hitBoxFeedback.PlayFeedbacks();
        Stats mouseClickStats = MoneyManager.Instance.GetMouseClickStats();
        var clickDamage = (int)mouseClickStats.TryToGetStat(Stat.HIT_DAMAGE);
        var moneyMultiplier = MouseClickManager.Instance.GetMultiplier();
        if(DecreaseHealthBy(mouseClickStats, clickDamage, moneyMultiplier) == BoxStatus.DEAD)
        {
            MouseClickManager.Instance.IncreaseCombo();
        } else
        {
            MouseClickManager.Instance.ResetCombo();
        }
        mouseClickStats.IncreaseStat(Stat.COUNT, 1);
    }

    private bool IsDead()
    {
        var isDead = _health <= 0;
        _boxStatus = isDead ? BoxStatus.DEAD : BoxStatus.ALIVE;
        return isDead;
    }
    
    public void InformLevelManagerAboutDeath()
    {
        LevelManager.Instance.CheckRemainingBoxes();
    }

    private void ChangeBlockColor()
    {
        int colors = _colors.Count;
        _spriteRenderer.color = _colors[_health % colors];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _hitParticles.transform.position = collision.GetContact(0).point;
            _hitParticles.transform.rotation = Quaternion.LookRotation(Vector3.forward, collision.GetContact(0).normal);
            _hitParticles.Play();
            _hitBoxFeedback.PlayFeedbacks();
        }
    }

}

public enum BoxStatus { ALIVE, DEAD }