using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private int _currentLevel;

    [SerializeField]
    private List<GameObject> _levels;

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

    private void Start()
    {
        EnableBlocks();
        StartCoroutine(CheckRemainingBlocks());
    }
    
    private IEnumerator CheckRemainingBlocks()
    {
        while (true)
        {
            if (!GetBlocksOfCurrentLevel().Find(block => block.gameObject.activeSelf))
            {
                print("No blocks left");
                IncreaseLevel();
            }
            print("Found at least one block");
            yield return new WaitForSeconds(0.1f);
        }

    }

    public void EnableBlocks()
    {
        foreach (var block in GetBlocksOfCurrentLevel())
        {
            block.SetStartHealth(_currentLevel);
            block.gameObject.SetActive(true);
        }
    }

    private List<Block> GetBlocksOfCurrentLevel()
    {
        var levels = _levels.Count;
        return _levels[_currentLevel % levels].GetComponentsInChildren<Block>(true).ToList();
    }

    public void IncreaseLevel()
    {
        print("Enable new blocks");
        _currentLevel++;
        EnableBlocks();
    }



}
