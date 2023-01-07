using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FeedBackItem;

public class FeedBackBox : MonoBehaviour
{
    [SerializeField] FeedBackItem _baseFeedBackItem;

    private List<FeedBackItem> _items = new List<FeedBackItem>();

    private void Awake()
    {
        _baseFeedBackItem.gameObject.SetActive(false);
    }

    public void AddFeedBack(FeedBackData feedBackData)
    {
        var clone = _baseFeedBackItem.Clone();
        clone.SetData(feedBackData);

        _items.Add(clone);
    }

    public void Clear()
    {
        foreach (var item in _items)
        {
            Destroy(item.gameObject);
        }
        _items.Clear();
    }
}
