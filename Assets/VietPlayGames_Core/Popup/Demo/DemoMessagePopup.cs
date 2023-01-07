using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DemoMessagePopup : BasePopup
{
    public static string DEMO_MESSAGE_POPUP = "DemoMessagePopup";

    public static void ShowPopup(string title, string content, Action handler = null)
    {
        Hashtable hashtable = new Hashtable();
        hashtable.Add("title", title);
        hashtable.Add("content", content);
        hashtable.Add("handler", handler);

        PopupController.Instance.ShowPopup(DEMO_MESSAGE_POPUP, hashtable);
    }

    public static void HidePopup()
    {
        PopupController.Instance.HidePopup(DEMO_MESSAGE_POPUP);
    }

    [SerializeField] TextMeshProUGUI _titleText;
    [SerializeField] TextMeshProUGUI _contentText;
    [SerializeField] Button _okeButton;

    private Action _handler;

    protected override void OnAddEvent()
    {
        base.OnAddEvent();
        _okeButton.onClick.AddListener(OnClickOkeButtonHandler);
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();
        _okeButton.onClick.RemoveListener(OnClickOkeButtonHandler);
    }

    private void OnClickOkeButtonHandler()
    {
        _handler?.Invoke();
        HidePopupSelf();
    }

    protected override void OnShow(object data)
    {
        base.OnShow(data);
        if (data != null)
        {
            Hashtable hashtable = data as Hashtable;
            _handler = (Action)hashtable["handler"];
            _titleText.text = (string)hashtable["title"];
            _contentText.text = (string)hashtable["content"];
        }
    }
}
