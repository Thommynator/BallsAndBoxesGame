using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsVisualizer : MonoBehaviour
{

    [SerializeField]    
    private Stats _stats;

    [Header("Prefabs")]
    [SerializeField]
    private StatsVisualizerField _fieldPrefab;

    [SerializeField]
    private GameObject _content;

    void Awake()
    {
        foreach (var item in _stats.GetStats())
        {
            Instantiate(_fieldPrefab, this.transform).Initialize(_stats, item.Key).transform.SetParent(_content.transform);
        }
    }
}
