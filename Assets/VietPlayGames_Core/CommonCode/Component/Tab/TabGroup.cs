using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<TabItem> _tabItems;
    [SerializeField] private int defaultIndex = 0;
    [SerializeField] bool _autoInit = true;

    public event Action<TabItem> SelectHandler;
    public event Action<int> OnChangeIndexHandler;
    public event Action<GameObject> OnContentActiveHandler;

    private TabItem _current;
    private bool _isStart;

    public TabItem Current
    {
        get
        {
            return _current;
        }
    }

    public int ActiveIndex
    {
        get
        {
            if (_current != null)
            {
                return _current.Index;
            }
            return -1;
        }
    }

    protected virtual void OnSelectTab(TabItem item)
    {
        if (SelectHandler != null)
        {
            SelectHandler(item);
        }

        OnChangeIndexHandler?.Invoke(item.Index);
    }

    private void Awake()
    {
        int index = 0;
        foreach (var tab in _tabItems)
        {
            tab.SetIndex(index++);
            tab.DeactiveTab();
            tab.OnSelectTabHandler += OnSelectTabHandler;
        }
    }

    private void Start()
    {
        _isStart = true;
        if(!_autoInit)
        {
            return;
        }
        StartCoroutine(IE_FixDefault());
    }

    private IEnumerator IE_FixDefault()
    {
        yield return null;
        if (_current == null)
        {
            if (defaultIndex != -1)
            {
                ActiveTab(defaultIndex);
            }
            else
            {
                ActiveTab(0);
            }
        }
    }

    private void OnEnable()
    {
        if (_isStart)
        {
            //StartCoroutine(IE_ActiveTab());
            if (_current == null)
            {
                if (defaultIndex != -1)
                {
                    ActiveTab(defaultIndex);
                }
                else
                {
                    ActiveTab(0);
                }
            }
        }
    }

    private IEnumerator IE_ActiveTab()
    {
        yield return null;
        if (_current == null)
        {
            if (defaultIndex != -1)
            {
                ActiveTab(defaultIndex);
            }
            else
            {
                ActiveTab(0);
            }
        }
    }

    private void OnSelectTabHandler(TabItem item)
    {
        if (_current != null && _current == item)
        {
            return;
        }

        if (_current != null)
        {
            _current.DeactiveTab();
        }

        _current = item;
        _current.ActiveTab();
        OnSelectTab(_current);
        OnContentActiveHandler?.Invoke(_current.Content);
    }

    public void ActiveTab(int index)
    {
        if (index < 0 || index >= _tabItems.Count)
        {
            return;
        }

        var tab = _tabItems[index];
        OnSelectTabHandler(tab);
    }

    public void AddTabItem(TabItem item)
    {
        _tabItems.Add(item);
        item.SetIndex(_tabItems.Count - 1);
        item.OnSelectTabHandler += OnSelectTabHandler;
        item.DeactiveTab();
    }

    public void Clear()
    {
        _tabItems.Clear();
        _current = null;
    }

    //editor
    private void OnValidate()
    {
        _tabItems.Clear();
        var tabs = GetComponentsInChildren<TabItem>();
        foreach (var t in tabs)
        {
            if (t.gameObject.activeSelf)
            {
                _tabItems.Add(t);
            }
        }
    }
}
