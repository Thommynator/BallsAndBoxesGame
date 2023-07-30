using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallVisualizer : MonoBehaviour
{
    public Stats ballStats;
    private Image image;
    private TextMeshProUGUI displayName;
    private TextMeshProUGUI description;


    void Start()
    { 
        image = GetComponentInChildren<Image>();
        image.transform.localScale = ballStats.scale;
        image.sprite = ballStats.sprite;
        image.color = ballStats.color;

        displayName = transform.Find("DisplayName").GetComponent<TextMeshProUGUI>();
        description = transform.Find("Description").GetComponent<TextMeshProUGUI>();

        displayName.text = ballStats.displayName;
        description.text = ballStats.description;
    }

}
