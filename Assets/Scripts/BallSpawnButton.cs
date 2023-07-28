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

    private void Start()
    {
        var imageOnButton = transform.Find("Button/BallSprite").GetComponent<Image>();
        imageOnButton.sprite = _ball.GetStats().sprite;
        imageOnButton.color = _ball.GetStats().color;
        imageOnButton.transform.localScale = _ball.GetStats().scale;
    }

    void Update()
    {
        var price = _ball.GetStats().TryToGetStat(Stat.PRICE);
        _progressBar.SetMaxValue((int)price);
    }

    public void TryToSpawnBall()
    {
        UpgradeManager.Instance.TryToBuyBall(_ball);
    }
}
