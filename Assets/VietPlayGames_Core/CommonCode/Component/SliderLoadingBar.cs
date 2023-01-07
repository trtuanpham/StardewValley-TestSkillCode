using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SliderLoadingBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [Range(0, 1)]
    [SerializeField] float _percent;
    [SerializeField] TextMeshProUGUI _percentText;

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
        if (slider != null)
        {
            slider.value = _percent;
            _percentText.text = _percent.ToString("P0");
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

