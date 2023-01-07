using UnityEngine;
using System.Collections;
using System;
using TMPro;
using UnityEngine.UI;

public class ConfirmPopup : BasePopup
{
    public static string NAME_POPUP = "ConfirmPopup";

    public static ConfirmPopup ShowPopup(string title, string content, Action<bool> handler = null)
    {
        Hashtable hashtable = new Hashtable();
        hashtable.Add("title", title);
        hashtable.Add("content", content);
        hashtable.Add("handler", handler);

        return PopupController.Instance.ShowPopup(NAME_POPUP, hashtable) as ConfirmPopup;
    }

    public static void HidePopup()
    {
        PopupController.Instance.HidePopup(NAME_POPUP);
    }

    [SerializeField] TextMeshProUGUI _titleText;
    [SerializeField] TextMeshProUGUI _contentText;
    [SerializeField] Button _okeButton;
    [SerializeField] Button _cancelButton;

    private Action<bool> _handler;

    protected override void OnAddEvent()
    {
        base.OnAddEvent();
        _okeButton.onClick.AddListener(OnClickOkeButtonHandler);
        _cancelButton.onClick.AddListener(OnClickCancelButtonHandler);
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();
        _okeButton.onClick.RemoveListener(OnClickOkeButtonHandler);
        _cancelButton.onClick.RemoveListener(OnClickCancelButtonHandler);
    }

    private void OnClickCancelButtonHandler()
    {
        _handler?.Invoke(false);
        HidePopupSelf();
    }

    private void OnClickOkeButtonHandler()
    {
        _handler?.Invoke(true);
        HidePopupSelf();
    }

    protected override void OnShow(object data)
    {
        base.OnShow(data);
        if (data != null)
        {
            Hashtable hashtable = data as Hashtable;
            _handler = (Action<bool>)hashtable["handler"];
            _titleText.text = (string)hashtable["title"];
            _contentText.text = (string)hashtable["content"];
        }
    }
}
