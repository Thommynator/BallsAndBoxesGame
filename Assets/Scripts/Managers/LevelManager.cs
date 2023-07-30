using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        DisableAllBoxes();
        EnableBlocksOfCurrentLevel();
        CheckRemainingBoxes();
    }

    [ContextMenu("CheckRemainingBlocks")]
    public void CheckRemainingBoxes()
    {
        if (!GetBoxesFor(_currentLevel).Find(block => block.GetBoxStatus() == BoxStatus.ALIVE))
        {
            IncreaseLevel();
        }
    }


    private void DisableAllBoxes()
    {
        for (int level = 0; level < _levels.Count; level++)
        {
            foreach (var block in GetBoxesFor(level))
            {
                block.gameObject.SetActive(false);
            }
        }
    }

    public void DisableBoxesOfCurrentLevel()
    {
        foreach (var block in GetBoxesFor(_currentLevel))
        {
            block.gameObject.SetActive(false);
        }
    }

    public void EnableBlocksOfCurrentLevel()
    {
        foreach (var box in GetBoxesFor(_currentLevel))
        {
            box.gameObject.SetActive(true);
            box.Activate(_currentLevel+1);
        }
    }

    private List<Box> GetBoxesFor(int level)
    {
        var levels = _levels.Count;
        return _levels[level % levels].GetComponentsInChildren<Box>(true).ToList();
    }

    public void IncreaseLevel()
    {
        DisableBoxesOfCurrentLevel();
        _currentLevel++;
        EnableBlocksOfCurrentLevel();
    }



}
