using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject _upgradePanel;

    void Start()
    {
        _upgradePanel.SetActive(false);
    }

    public void TogglePanel()
    {
        if (_upgradePanel.activeSelf)
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
        _upgradePanel.SetActive(true);
    }

    public void HidePanel()
    {
        _upgradePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
