using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField]
    private RawImage _image;

    [SerializeField]
    private float _scrollSpeedX, _scrollSpeedY;

    // Update is called once per frame
    void Update()
    {
        _image.uvRect = new Rect(_image.uvRect.position + new Vector2(_scrollSpeedX, _scrollSpeedY) * Time.deltaTime, _image.uvRect.size);
    }
}
