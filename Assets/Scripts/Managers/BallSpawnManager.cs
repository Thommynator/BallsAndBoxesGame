using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnManager : MonoBehaviour
{

    [SerializeField]
    private Transform _spawnArea;

    public static BallSpawnManager Instance;

    private GameObject _parentObject;

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
        _parentObject = GameObject.Find("Balls");
    }

    public Ball SpawnBall(Ball ballPrefab)
    {
        var width = _spawnArea.localScale.x;
        var height = _spawnArea.localScale.y;
        var x = Random.Range(-width / 2, width / 2);
        var y = Random.Range(-height / 2, height / 2);
        var randomPosition = _spawnArea.transform.position + new Vector3(x, y, _spawnArea.position.z);
        var ball = Instantiate(ballPrefab, randomPosition, Quaternion.identity);
        ball.transform.SetParent(_parentObject.transform);

        ball.GetStats().IncreaseStat(Stat.COUNT, 1);

        return ball;
    }
}
