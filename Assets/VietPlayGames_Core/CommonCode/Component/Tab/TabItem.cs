using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabItem : BaseMonoBehaviour
{
    public int Index { get; private set; }

    public Action<TabItem> OnSelectTabHandler;
    public Action<bool> OnUpdateSelectTabHandler;

    [SerializeField] protected GameObject _content;
    [SerializeField] private Button _tabButton;
    [SerializeField] bool _isClone = true;

    public bool IsTabActive { get; private set; }

    public GameObject Content { get; private set; }

    private void Awake()
    {
        if (_content != null)
        {
            //_tabContent = (ITabContent)_content.GetComponent(typeof(ITabContent));
            _content.gameObject.SetActive(false);
        }
    }

    protected override void OnAddEvent()
    {
        base.OnAddEvent();
        _tabButton.onClick.AddListener(OnClickTabButtonHandler);
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();
        _tabButton.onClick.AddListener(OnClickTabButtonHandler);
    }

    private void OnClickTabButtonHandler()
    {
        OnSelect();
    }

    protected virtual void OnSelect()
    {
        OnSelectTabHandler?.Invoke(this);
    }

    protected virtual void OnActiveTab() { }

    protected virtual void OnDeactiveTab() { }

    public void ActiveTab()
    {
        IsTabActive = true;
        OnActiveTab();

        if (_content != null)
        {
            if (_isClone)
            {
                Content = _content.Clone();
            }
            else
            {
                Content = _content;
                Content.SetActive(true);
            }
            Content.transform.ResetTransform();
        }

        //  StartCoroutine(IE_Fix());

        OnUpdateSelectTabHandler?.Invoke(true);
    }

    public void DeactiveTab()
    {
        IsTabActive = false;
        OnDeactiveTab();

        if (Content != null)
        {
            if (_isClone)
            {
                Destroy(Content);
            }
            else
            {
                Content.SetActive(false);
            }
        }

        OnUpdateSelectTabHandler?.Invoke(false);
    }

    public void SetIndex(int index)
    {
        Index = index;
    }

    private void OnValidate()
    {
        if (_tabButton == null)
        {
            _tabButton = GetComponent<Button>();
        }
    }
}
