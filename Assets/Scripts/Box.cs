using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using MoreMountains.Feedbacks;

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

    [SerializeField]
    private MMF_Player _clickOnBoxFeedback;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        _clickOnBoxFeedback = GameObject.Find("Feedbacks/MouseClickOnBoxFeedback").GetComponent<MMF_Player>();
        _health = _startHealth;
        UpdateVisuals();
        gameObject.SetActive(true);
    }

    public void SetStartHealth(int startHealth)
    {
        _startHealth = startHealth;
        _health = _startHealth;
        UpdateVisuals();
    }

    public BlockStatus DecreaseHealthBy(int damage)
    {
        var realDamage = Mathf.Min(damage, _health);
        MoneyManager.Instance.IncreaseMoneyBy(realDamage);
        _health -= realDamage;
        if (IsDead())
        {
            gameObject.SetActive(false);
            return BlockStatus.DEAD;
        } else
        {
            UpdateVisuals();
            return BlockStatus.ALIVE;
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
        return _health <= 0;
    }

    private void ChangeBlockColor()
    {
        int colors = _colors.Count;
        _spriteRenderer.color = _colors[_health % colors];

    }

}

public enum BlockStatus { ALIVE, DEAD }