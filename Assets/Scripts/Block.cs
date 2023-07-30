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

    public BlockStatus DecreaseHealthBy(int damage)
    {
        var realDamage = Mathf.Min(damage, _health);
        UpgradeManager.Instance.IncreaseMoneyBy(realDamage);
        _health -= realDamage;
        if (IsDead())
        {
            Debug.Log("Block is dead");
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
        DecreaseHealthBy(UpgradeManager.Instance.GetClickDamange());
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