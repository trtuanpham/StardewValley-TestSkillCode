using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButtonControl : BaseMonoBehaviour
{
    [SerializeField]
    protected Button _button;

    protected override void OnInit()
    {
        base.OnInit();

        if (_button == null)
        {
            Debug.LogError("Button " + gameObject.name + "is null");
        }
    }

    protected override void OnAddEvent()
    {
        base.OnAddEvent();

        _button.onClick.AddListener(OnClickButtonHandler);
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();

        _button.onClick.RemoveListener(OnClickButtonHandler);
    }

    protected virtual void OnClickButtonHandler() { }

    protected virtual void OnValidate()
    {
        _button = gameObject.GetComponent<Button>();
    }
}
