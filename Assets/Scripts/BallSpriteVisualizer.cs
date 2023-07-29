using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpriteVisualizer : MonoBehaviour
{
    public BallStats ballStats;

    void Start()
    {
        transform.localScale = ballStats.scale;
        GetComponent<Image>().sprite = ballStats.sprite;
        GetComponent<Image>().color = ballStats.color;
    }

}
