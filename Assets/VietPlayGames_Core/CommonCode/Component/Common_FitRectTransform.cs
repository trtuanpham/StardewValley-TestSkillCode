using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Common_FitRectTransform : MonoBehaviour
{
    [SerializeField] private RectTransform _target;
    [SerializeField] private bool _syncPosition = false;
    [SerializeField] bool _useX = true;
    [SerializeField] bool _useY = true;
    [SerializeField] private bool _syncSize = false;
    [SerializeField] bool _useWidth = true;
    [SerializeField] bool _useHeigh = true;
    [SerializeField] float _xOffset = 0;
    [SerializeField] float _yOffset = 0;

    private Vector3 _position;
    private Vector2 _size;

    private RectTransform rectTransform
    {
        get
        {
            return transform as RectTransform;
        }
    }

    private void Start()
    {
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_target != null && (_target.rect.size != _size || _target.position != _position))
        {



            if (_syncPosition)
            {
                _position = rectTransform.position;
                if(_useX)
                {
                    _position.x = _target.position.x;
                }

                if (_useY)
                {
                    _position.y = _target.position.y;
                }
                rectTransform.position = _position;
            }

            if (_syncSize)
            {
                _size = rectTransform.rect.size;
      
                if (_useWidth)
                {
                    _size.x = _target.rect.size.x;
                }

                if (_useHeigh)
                {
                    _size.y = _target.rect.size.y;
                }

                _size.x += _xOffset;
                _size.y += _yOffset;

                rectTransform.sizeDelta = _size;
            }
            // rectTransform.rect.Set(_target.rect.position.x, _target.position.y, _target.rect.size.x, _target.rect.size.y);
        }

        _position = rectTransform.position;
        _size = rectTransform.sizeDelta;
    }
}
