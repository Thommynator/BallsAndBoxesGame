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

    private SpriteRenderer _spriteRenderer;

    private BoxStatus _boxStatus;

    [SerializeField]
    private MMF_Player _clickOnBoxFeedback;

    [SerializeField]
    private MMF_Player _openBoxFeedback;

    [SerializeField]
    private MMF_Player _closeBoxFeedback;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        _health = _startHealth;
        UpdateVisuals();
        Activate(_startHealth);
        
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

    public void Activate(int startHealth)
    {
        SetStartHealth(startHealth);
        _openBoxFeedback.PlayFeedbacks();
        _boxStatus = BoxStatus.ALIVE;
    }

    public BoxStatus DecreaseHealthBy(int damage)
    {
        var realDamage = Mathf.Min(damage, _health);
        MoneyManager.Instance.IncreaseMoneyBy(realDamage);
        _health -= realDamage;
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
        DecreaseHealthBy(MoneyManager.Instance.GetClickDamange());
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

}

public enum BoxStatus { ALIVE, DEAD }