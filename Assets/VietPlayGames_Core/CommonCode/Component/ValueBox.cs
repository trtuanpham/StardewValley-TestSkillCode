using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ValueBox : MonoBehaviour
{
    [SerializeField] private string _content = "{0}";
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] bool _hasEffect = true;

    private long _currentValue;
    private bool _isFirst = true;

    private void Start()
    {
        DOTween.Kill(this);
        UpdateContent(_currentValue);
    }

    public void UpdateValue(long value, float delay = 0)
    {
        var v = _currentValue;
        _currentValue = value;
        if (!_isFirst && _hasEffect)
        {
            DOTween.Kill(this);
            DOTween.To(() => v, x => v = x, _currentValue, _isFirst ? 0f : 0.5f).SetDelay(value < 0 ? 0f : delay).OnUpdate(() =>
                     {
                         UpdateContent(v);
                     }).SetTarget(this);
        }
        else
        {
            UpdateContent(_currentValue);
        }
        _isFirst = false;
    }

    public void SetData(long value, float delay = 0)
    {
        UpdateValue(value, delay);
    }

    public void OnDestroy()
    {
        DOTween.Kill(this);
    }

    private void UpdateContent(long value)
    {
        if (string.IsNullOrEmpty(_content))
        {
            _valueText.text = value.ToString("N0");
        }
        else
        {
            _valueText.text = string.Format(_content, value.ToString("N0"));// value.ToString();
        }
    }
}
