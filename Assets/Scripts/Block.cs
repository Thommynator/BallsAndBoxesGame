using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Block : MonoBehaviour
{

    [SerializeField]
    private int _startHealth;

    private int _health;

    [SerializeField]
    private TextMeshProUGUI _healthText;

    [SerializeField]    
    private List<Color> _colors;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
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

    public void DecreaseHealthBy(int damage)
    {
        var realDamage = Mathf.Min(damage, _health);
        UpgradeManager.Instance.IncreaseMoneyBy(realDamage);
        _health -= realDamage;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        CheckIfDead();
        _healthText.text = _health.ToString();
        ChangeBlockColor();
    }

    private void OnMouseDown()
    {
        DecreaseHealthBy(UpgradeManager.Instance.GetClickDamange());
    }

    private void CheckIfDead()
    {
        if (_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void ChangeBlockColor()
    {
        int colors = _colors.Count;
        _spriteRenderer.color = _colors[_health % colors];

    }



}