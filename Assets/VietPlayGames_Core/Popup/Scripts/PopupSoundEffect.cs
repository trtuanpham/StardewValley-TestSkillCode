using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSoundEffect : BaseMonoBehaviour
{
    [SerializeField] string _popupOpenSoundId = "PopupOpen";
    [SerializeField] string _popupCloseSoundId = "PopupClose";
    [SerializeField] BasePopup _popup;

    protected override void OnAddEvent()
    {
        base.OnAddEvent();
        _popup.OnOpenPopupHandler += _popup_OnOpenPopupHandler;
        _popup.OnClosePopupHandler += _popup_OnClosePopupHandler;
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();
        _popup.OnOpenPopupHandler -= _popup_OnOpenPopupHandler;
        _popup.OnClosePopupHandler -= _popup_OnClosePopupHandler;
    }

    private void _popup_OnClosePopupHandler()
    {
        Sound2D.Play(_popupCloseSoundId);
    }

    private void _popup_OnOpenPopupHandler()
    {
        Sound2D.Play(_popupOpenSoundId);
    }

    private void OnValidate()
    {
        if(_popup == null)
        {
            _popup = GetComponent<BasePopup>();
        }
    }
}
