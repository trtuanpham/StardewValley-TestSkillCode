using UnityEngine;
using System.Collections;
using static FeedBackItem;
using System;

public class AvatarShopChatingPopup : BasePopup
{
    public static string NAME_POPUP = "AvatarShopChatingPopup";
    public static AvatarShopChatingPopup ShowPopup()
    {
        Hashtable hashtable = new Hashtable();
        return PopupController.Instance.ShowPopup(NAME_POPUP, hashtable) as AvatarShopChatingPopup;
    }

    public static void HidePopup()
    {
        PopupController.Instance.HidePopup(NAME_POPUP);
    }

    //CODE_HERE
    [SerializeField] ChatBox _chatBox;
    [SerializeField] FeedBackBox _feedBackBox;

    protected override void OnShow(object data)
    {
        base.OnShow(data);
        if (data != null)
        {
            Hashtable hashtable = data as Hashtable;
        }

        Chating("Do you want to buy something?", "Yes, I want", () =>
        {
            HidePopupSelf();
            ShopPopup.ShowPopup();
        }, "Nope", () =>
        {
            Clear();
            Chating("Ok, See you Again, Bye!!!", "Bye!!!", () =>
            {
                HidePopupSelf();
            });
        });
    }

    private void Chating(string content, string feedBack1, Action callback1)
    {
        _chatBox.gameObject.SetActive(true);
        _chatBox.SetData(content);
        _feedBackBox.Clear();
        _feedBackBox.AddFeedBack(new FeedBackData() { content = feedBack1, handler = callback1 });
    }

    private void Chating(string content, string feedBack1, Action callback1, string feedBack2, Action callback2)
    {
        _chatBox.gameObject.SetActive(true);
        _chatBox.SetData(content);
        _feedBackBox.Clear();
        _feedBackBox.AddFeedBack(new FeedBackData() { content = feedBack1, handler = callback1 });
        _feedBackBox.AddFeedBack(new FeedBackData() { content = feedBack2, handler = callback2 });
    }

    private void Clear()
    {
        _chatBox.gameObject.SetActive(false);
        _feedBackBox.Clear();
    }

    protected override void OnHide()
    {
        base.OnHide();
    }
}
