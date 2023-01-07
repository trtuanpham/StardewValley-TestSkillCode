using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using BugBoxGames.EnhancedEffect;

[DisallowMultipleComponent]
public class BasePopup : BaseMonoBehaviour
{
    public float Delay { set; get; }
    public bool HasSupportBackKey { get; private set; }

    //public virtual string PopupName
    //{
    //    get
    //    {
    //        throw new ApplicationException(gameObject.name + " need set name of popup.");
    //    }
    //}

    [SerializeField] private Transform _popupView;
    [SerializeField] private List<BaseEffect> _effects;
    [SerializeField] protected Button _closeButton;

    public event Action OnOpenPopupHandler;
    public event Action OnClosePopupHandler;

    protected PopupController _controller;

    public void InitRoot(PopupController controller)
    {
        _controller = controller;
        HasSupportBackKey = _closeButton != null;
    }

    protected void HidePopupSelf()
    {
        _controller.HidePopup(this);
    }

    protected override void OnAddEvent()
    {
        base.OnAddEvent();

        if (_closeButton != null)
        {
            _closeButton.onClick.AddListener(OnClickCloseButtonHandler);
        }
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();

        if (_closeButton != null)
        {
            _closeButton.onClick.RemoveListener(OnClickCloseButtonHandler);
        }
    }

    protected virtual void OnClickCloseButtonHandler()
    {
        HidePopupSelf();
    }

    /// Only run on editor mode
    protected virtual void OnValidate()
    {
        foreach (Transform t in transform)
        {
            if (t.name == "PopupView")
            {
                _popupView = t;
                break;
            }
        }

        //get effects
        if (_effects != null)
        {
            _effects.Clear();
        }else
        {
            _effects = new List<BaseEffect>();
        }
        // _effects = new List<BaseEffect>();
        var e1 = GetComponents<BaseEffect>();
        if (e1 != null)
        {
            _effects.AddRange(e1);
        }

        if (_popupView != null)
        {
            var e2 = _popupView.GetComponents<BaseEffect>();
            if (e2 != null)
            {
                _effects.AddRange(e2);
            }
        }
    }

    public void Show(object data, float delay)
    {
        StartCoroutine(IE_Show(data, delay));
    }

    private IEnumerator IE_Show(object data, float delay)
    {
        Delay = delay;
        Init();
        OnShowAnimation();
        yield return null;
        OnShow(data);
        AddEvent();
    }

    public void Hide()
    {
        OnHide();
        OnHideAnimation();
        RemoveEvent();
    }

    protected virtual void OnShow(object data) {
        StartCoroutine(IE_OpenPopup());
    }

    protected virtual void OnHide() {
        OnClosePopupHandler?.Invoke();
    }

    private IEnumerator IE_OpenPopup()
    {
        yield return null;
        OnOpenPopupHandler?.Invoke();
    }

    protected virtual void OnShowAnimation()
    {
        //gameObject.SetActive(true);
        foreach (var e in _effects)
        {
            e.TurnOffAuto();
            e.Delay = Delay;
            e.ShowEffect();
        }

    }

    protected virtual void OnHideAnimation()
    {
        float delay = 0;
        foreach (var e in _effects)
        {
            e.HideEffect();
            if (e.FullTimeEffect > delay)
            {
                delay = e.FullTimeEffect;
            }
        }
        Destroy(gameObject,delay);
    }
}
