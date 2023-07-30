using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats", order = 1)]
public class Stats : ScriptableObject
{
    public List<StatKeyValuePair> initialStats = new List<StatKeyValuePair>();
    public Sprite sprite;
    public Vector3 scale;
    public Color color;
    public List<AudioClip> hitSounds;

    private Dictionary<Stat, float> _stats = new Dictionary<Stat, float>();

    public void LoadStats()
    {
        _stats = new Dictionary<Stat, float>();
        foreach (var stat in initialStats)
        {
            _stats.Add(stat.key, stat.value);
        }
    }

    public float TryToGetStat(Stat stat)
    {
        if (_stats.TryGetValue(stat, out float value))
        {
            return value;
        }
        Debug.LogError($"No stat value found for {stat} on {this.name}");
        return 0;
    }

    public void IncreaseStat(Stat stat, float value)
    {
        if (_stats.ContainsKey(stat))
        {
            _stats[stat] += value;
        }
        else
        {
            Debug.LogError($"Not found! Can't increase stat {stat} on {this.name}");
        }
    }

    public void SetStat(Stat stat, float value)
    {
        if (_stats.ContainsKey(stat))
        {
            _stats[stat] = value;
        }
        else
        {
            Debug.LogError($"Not found! Can't set new value for stat {stat} on {this.name}");
        }
    }
}

[Serializable]
public class StatKeyValuePair
{
    public Stat key;
    public float value;
}