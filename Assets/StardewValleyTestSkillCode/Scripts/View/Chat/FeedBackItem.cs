using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static FeedBackItem;

public class FeedBackItem : BaseMonoBehaviour
{
    [SerializeField] TextMeshProUGUI _content;
    [SerializeField] Button _button;

    private FeedBackData _feedBackData;

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

    private void OnClickButtonHandler()
    {
        _feedBackData.handler?.Invoke();
    }

    public void SetData(FeedBackData feedBackData)
    {
        _feedBackData = feedBackData;
        _content.text = _feedBackData.content;
    }

    public class FeedBackData
    {
        public string content;
        public Action handler;
    }
}
