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
        HideAllBoxes();
        EnableBoxesOfCurrentLevel();
        CheckRemainingBoxes();
    }

    [ContextMenu("CheckRemainingBlocks")]
    public void CheckRemainingBoxes()
    {
        if (!GetBoxesFor(_currentLevel).Find(box => box.GetBoxStatus() == BoxStatus.ALIVE))
        {
            IncreaseLevel();
        }
    }


    private void HideAllBoxes()
    {
        for (int level = 0; level < _levels.Count; level++)
        {
            foreach (var box in GetBoxesFor(level))
            {
                box.Hide();
            }
        }
    }

    public void HideBoxesOfCurrentLevel()
    {
        foreach (var block in GetBoxesFor(_currentLevel))
        {
            block.Hide();
        }
    }

    public void EnableBoxesOfCurrentLevel()
    {
        foreach (var box in GetBoxesFor(_currentLevel))
        {
            box.Activate(_currentLevel+1);
        }
    }

    private List<Box> GetBoxesFor(int level)
    {
        var levels = _levels.Count;
        return _levels[(level % levels)].GetComponentsInChildren<Box>(true).ToList();
    }

    public List<Box> GetCurrentBoxes()
    {
        return GetBoxesFor(_currentLevel);
    } 

    public void IncreaseLevel()
    {
        HideBoxesOfCurrentLevel();
        _currentLevel++;
        EnableBoxesOfCurrentLevel();
    }



}
