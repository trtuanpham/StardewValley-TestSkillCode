using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Image _image;
    [Range(0, 1)]
    [SerializeField] float _percent;
    [SerializeField] float _maxSize;
    [SerializeField] float _minSize = 30;

    public float GetValue()
    {
        return _percent;
    }

    public void SetValue(float percent)
    {
        if (percent < 0)
        {
            percent = 0;
        }

        if (percent > 1)
        {
            percent = 1;
        }

        _percent = percent;
    }

    private void Update()
    {
        if (_image != null)
        {
            var size = _image.rectTransform.sizeDelta;
            size.x = _maxSize * _percent;
            if (size.x < _minSize)
            {
                size.x = _minSize;
            }
            _image.rectTransform.sizeDelta = size;
        }
    }

    public void SetLoading(float percent, float time = 0.5f)
    {
        DOTween.Kill(this);
        DOTween.To(x =>
        {
            SetValue(x);
        }, GetValue(), percent, time).SetTarget(this);
    }

    public void Stop()
    {
        DOTween.Kill(this);
    }
}
