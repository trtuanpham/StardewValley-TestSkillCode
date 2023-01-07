using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.UI;

public class PopupController : Singleton<PopupController>
{
    public event Action OnUpdatePopupHandler;

    private List<BasePopup> _activePopups = new List<BasePopup>();
    //protected Canvas _canvas;

    private object _lock = new object();

    public bool HasActivePopup
    {
        get
        {
            return _activePopups.Count > 0;
        }
    }

    private void Start()
    {
        var canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 10;
        gameObject.AddComponent<GraphicRaycaster>();
        var canvasScaler = gameObject.AddComponent<CanvasScaler>();
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        canvasScaler.referenceResolution = new Vector2(900, 900);
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }

    public bool IsActive(string popupName)
    {
        foreach (var popup in _activePopups)
        {
            if (popup.name == popupName)
            {
                return true;
            }
        }
        return false;
    }

    public BasePopup ShowPopup(string popupName, object data = null, float delay = 0.3f)
    {
        var popup = UnityUtils.SpawnResources<BasePopup>("Popups/" + popupName);
        if (popup == null)
        {
            throw new ApplicationException("not found: " + popupName);
        }

        popup.transform.SetParent(transform);
        popup.InitRoot(this);
        popup.transform.localPosition = Vector3.zero;
        popup.transform.localScale = Vector3.one;
        popup.name = popupName;

        var rectTransform = popup.transform as RectTransform;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;

        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        _activePopups.Add(popup);

        popup.Show(data, delay);
        popup.transform.SetAsLastSibling();

        OnUpdatePopupHandler?.Invoke();
        return popup;
    }

    public void HidePopup(string popupName)
    {
        lock (_lock)
        {
            for (int i = _activePopups.Count - 1; i >= 0; i--)
            {
                var popup = _activePopups[i];
                if (popup != null && popup.name == popupName)
                {
                    _activePopups.RemoveAt(i);
                    popup.Hide();
                    OnUpdatePopupHandler?.Invoke();
                    break;
                }
            }
        }
    }

    public void HidePopup(BasePopup popup)
    {
        lock (_lock)
        {
            _activePopups.Remove(popup);
            popup.Hide();
            OnUpdatePopupHandler?.Invoke();
        }
    }

    public void HideAnyPopup()
    {
        lock (_lock)
        {
            for (int i = _activePopups.Count - 1; i >= 0; i--)
            {
                var popup = _activePopups[i];
                popup.Hide();
            }
            _activePopups.Clear();
        }

        OnUpdatePopupHandler?.Invoke();
    }

    public BasePopup GetLastPopup()
    {
        if (_activePopups.Count <= 0)
        {
            return null;
        }

        var popup = _activePopups[_activePopups.Count - 1];
        return popup;
    }
}
