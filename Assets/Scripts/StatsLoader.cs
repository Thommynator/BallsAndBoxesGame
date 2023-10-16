using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsLoader : MonoBehaviour
{
    public List<Stats> stats;

    void Awake()
    {
        foreach (var stat in stats)
        {
            stat.LoadStats();
        }
    }

}
