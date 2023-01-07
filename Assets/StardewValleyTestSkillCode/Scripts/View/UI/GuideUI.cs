using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideUI : BaseMonoBehaviour
{
    [SerializeField] GameObject _root;
    protected override void OnAddEvent()
    {
        base.OnAddEvent();

        PopupController.Instance.OnUpdatePopupHandler += PopupController_OnUpdatePopupHandler;
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();

        PopupController.Instance.OnUpdatePopupHandler -= PopupController_OnUpdatePopupHandler;
    }

    private void PopupController_OnUpdatePopupHandler()
    {
        if (PopupController.Instance.HasActivePopup)
        {
            _root.SetActive(false);
        }
        else
        {
            _root.SetActive(true);
        }
    }
}
