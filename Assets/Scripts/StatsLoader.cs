using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsLoader : MonoBehaviour
{
    public List<BallStats> stats;

    void Start()
    {
        foreach (var stat in stats)
        {
            stat.LoadStats();
        }
    }

}
