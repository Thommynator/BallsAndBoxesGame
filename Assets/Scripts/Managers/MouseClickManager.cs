using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using TMPro;
using MoreMountains.Feedbacks;

public class MouseClickManager : MonoBehaviour
{

    [HideInInspector]
    public static MouseClickManager Instance;

    [SerializeField]
    private int _minComboValue;

    [SerializeField]
    private int _maxComboValue;

    [SerializeField]
    private float _currentComboValue;

    [SerializeField]
    private float _decayRate;

    [SerializeField]
    private float _decayIntervalInSeconds;

    [SerializeField]
    private float _increaseRate;

    [SerializeField]
    private MMProgressBar _progressBar;

    [SerializeField]
    private TextMeshProUGUI _multiplierText;

    [SerializeField]
    private MMF_Player _resetFeedback;

    [SerializeField]
    private MMF_Player _multiplierIncreaseFeedback;

    public float xOffset, yOffset;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    void Start()
    {
        SetComboValue(_minComboValue);
    }

    void Update()
    {
        _progressBar.gameObject.transform.position = Input.mousePosition + new Vector3(xOffset, yOffset, 0);
    }

    [ContextMenu("Increase")]
    public void IncreaseCombo()
    {
        StopAllCoroutines();
        StartCoroutine(Decay()); // restart decay counter, i.e. it won't decay if the player keeps clicking
        SetComboValue(_currentComboValue + _increaseRate);
        print($"Increase combo value to {_currentComboValue} ({GetMultiplier()}x)");
    }

    [ContextMenu("Reset")]
    public void ResetCombo()
    {
        if(_currentComboValue > 1)
        {
            _resetFeedback.PlayFeedbacks();
        }
        SetComboValue(_minComboValue);
    }

    private void SetComboValue(float value)
    {
        var previousMultiplierLevel = GetMultiplier();
        _currentComboValue = Mathf.Clamp(value, _minComboValue, _maxComboValue);
        _progressBar.UpdateBar(_currentComboValue, _minComboValue, _maxComboValue);

        if(GetMultiplier() > previousMultiplierLevel)
        {
            _multiplierIncreaseFeedback.PlayFeedbacks();
        }
        
        ToggleText();
        _multiplierText.text = $"{GetMultiplier()}x";
    }

    private void ToggleText()
    {
        if (_currentComboValue > 1)
        {
            _multiplierText.gameObject.SetActive(true);
        }
        else
        {
            _multiplierText.gameObject.SetActive(false);
        }
    }

    public int GetMultiplier()
    {
        return Mathf.FloorToInt(_currentComboValue);
    }

    private IEnumerator Decay()
    {
        while(true)
        {
            yield return new WaitForSeconds(_decayIntervalInSeconds);
            SetComboValue(_currentComboValue - _decayRate);
        }
    }
}
