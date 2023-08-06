using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoxSO", menuName = "ScriptableObjects/Box", order = 1)]
public class BoxSO : ScriptableObject
{

    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();

    public Sprite GetRandomSprite()
    {
        return sprites[Random.Range(0, sprites.Count)];
    }
    
}
