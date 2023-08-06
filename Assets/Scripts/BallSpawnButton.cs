using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawnButton : MonoBehaviour
{
    [SerializeField] 
    private Ball _ball;

    [SerializeField]
    private ProgressBar _progressBar;

    [SerializeField]
    private MMF_Player _successfulPurchaseFeedback;

    [SerializeField]
    private MMF_Player _failedPurchaseFeedback;

    private void Start()
    {
        var imageOnButton = transform.Find("Button/BallSprite").GetComponent<Image>();
        imageOnButton.sprite = _ball.GetStats().sprite;
        imageOnButton.transform.localScale = _ball.GetStats().scale;
    }

    void Update()
    {
        var price = _ball.GetStats().TryToGetStat(Stat.PRICE);
        _progressBar.SetMaxValue((int)price);
    }

    public void TryToSpawnBall()
    {
        if (MoneyManager.Instance.TryToBuyBall(_ball))
        {
            _successfulPurchaseFeedback.PlayFeedbacks();
        } else
        {
            _failedPurchaseFeedback.PlayFeedbacks();
        }
    }
}
