using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabEffect : BaseMonoBehaviour
{
    [SerializeField] TabItem _tabItem;
    [SerializeField] GameObject _activeObject;
    [SerializeField] GameObject _deactiveObject;

    protected override void OnAddEvent()
    {
        base.OnAddEvent();
        _tabItem.OnUpdateSelectTabHandler += _tabItem_OnUpdateSelectTabHandler;
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();
        _tabItem.OnUpdateSelectTabHandler -= _tabItem_OnUpdateSelectTabHandler;
    }

    protected override void OnInit()
    {
        base.OnInit();
        UpdateActive();
    }

    private void _tabItem_OnUpdateSelectTabHandler(bool isActive)
    {
        UpdateActive();
    }

    private void UpdateActive()
    {
        _activeObject.SetActive(_tabItem.IsTabActive);
        _deactiveObject.SetActive(!_tabItem.IsTabActive);
    }

    private void OnValidate()
    {
        if (_tabItem == null)
        {
            _tabItem = GetComponent<TabItem>();
        }
    }
}
