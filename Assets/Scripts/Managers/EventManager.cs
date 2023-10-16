using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public static EventManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Action onUpgrade;

    public void Upgrade()
    {
        onUpgrade?.Invoke();
    }

    public Action<Ball> onBallSpawned;
    public void BallSpawned(Ball ball)
    {
        onBallSpawned?.Invoke(ball);
    }

    public Action<Stats> onMouseClickDamage;

    public void MouseClickDamage(Stats stats)
    {
        onMouseClickDamage?.Invoke(stats);
    }

    public Action<Stats, int> onDamageDealt;

    public void DamageDealt(Stats damageDealer, int damage)
    {
        onDamageDealt?.Invoke(damageDealer, damage);
    }

 }
