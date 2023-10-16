using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject _statisticsPanel;

    [SerializeField]
    private MMF_Player _openPanelFeedback;

    [SerializeField]
    private MMF_Player _closePanelFeedback;

    void Start()
    {
        _statisticsPanel.SetActive(false);
    }

    public void TogglePanel()
    {
        if (_statisticsPanel.activeSelf)
        {
            HidePanel();
        }
        else
        {
            ShowPanel();
        }
    }

    public void ShowPanel()
    {
        _openPanelFeedback.PlayFeedbacks();
    }

    public void HidePanel()
    {
        _closePanelFeedback.PlayFeedbacks();
    }
}
