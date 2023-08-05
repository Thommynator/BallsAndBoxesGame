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

 }
