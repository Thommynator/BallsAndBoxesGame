using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private int _currentLevelIndex;

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
        if (!GetBoxesFor(_currentLevelIndex).Find(box => box.GetBoxStatus() == BoxStatus.ALIVE))
        {
            IncreaseLevel();
        }
    }

    public List<Box> GetCurrentBoxes()
    {
        return GetBoxesFor(_currentLevelIndex);
    }

    public int GetLevelNumber()
    {
        return _currentLevelIndex + 1;
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

    private void HideBoxesOfCurrentLevel()
    {
        foreach (var block in GetBoxesFor(_currentLevelIndex))
        {
            block.Hide();
        }
    }

    private void EnableBoxesOfCurrentLevel()
    {
        foreach (var box in GetBoxesFor(_currentLevelIndex))
        {
            box.Activate(_currentLevelIndex+1);
        }
    }

    private List<Box> GetBoxesFor(int level)
    {
        var levels = _levels.Count;
        return _levels[(level % levels)].GetComponentsInChildren<Box>(true).ToList();
    }

    private void IncreaseLevel()
    {
        HideBoxesOfCurrentLevel();
        _currentLevelIndex++;
        EnableBoxesOfCurrentLevel();
    }

    public void LeaveGame()
    {
        Application.Quit();
    }



}
