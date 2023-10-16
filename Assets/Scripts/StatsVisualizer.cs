using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
<<<<<<< HEAD
=======
using static UnityEditor.Progress;
>>>>>>> master

public class StatsVisualizer : MonoBehaviour
{

    [SerializeField]    
    private Stats _stats;

    [Header("Prefabs")]
    [SerializeField]
    private StatsVisualizerField _fieldPrefab;

    [SerializeField]
    private GameObject _content;

<<<<<<< HEAD
    void Awake()
=======
    void Start()
>>>>>>> master
    {
        foreach (var item in _stats.GetStats())
        {
            Instantiate(_fieldPrefab, this.transform).Initialize(_stats, item.Key).transform.SetParent(_content.transform);
        }
    }

}
