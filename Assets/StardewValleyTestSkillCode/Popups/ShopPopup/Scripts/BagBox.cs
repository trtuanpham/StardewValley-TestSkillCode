using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagBox : BaseMonoBehaviour
{
    [SerializeField] BagBoxItem _baseBagBoxItem;

    private BagBoxItem _selectedItem;
    private List<BagBoxItem> _items = new List<BagBoxItem>();

    private void Awake()
    {
        _baseBagBoxItem.gameObject.SetActive(false);
    }

    protected override void OnInit()
    {
        base.OnInit();

        LoadBags();
    }

    protected override void OnAddEvent()
    {
        base.OnAddEvent();

        BagController.Instance.OnAddNewBagHandler += BagController_OnAddNewBagHandler;
        BagController.Instance.OnRemoveBagHandler += BagController_OnRemoveBagHandler;
        BagController.Instance.OnUpdateBagHandler += BagController_OnUpdateBagHandler;
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();

        BagController.Instance.OnAddNewBagHandler -= BagController_OnAddNewBagHandler;
        BagController.Instance.OnRemoveBagHandler -= BagController_OnRemoveBagHandler;
        BagController.Instance.OnUpdateBagHandler -= BagController_OnUpdateBagHandler;
    }

    private void BagController_OnUpdateBagHandler(BagController.BagData bagData)
    {
        foreach (var item in _items)
        {
            if (item.BagData.avatarId == bagData.avatarId)
            {
                item.SetData(bagData);
                return;
            }
        }
    }

    private void BagController_OnRemoveBagHandler(AvatarType avatarType, string avatarId)
    {
        AvatarController.Instance.UnEquipAvatar(avatarType, avatarId);

        foreach (var item in _items)
        {
            if(item.BagData.avatarId == avatarId)
            {
                Destroy(item.gameObject);
                _items.Remove(item);
                return;
            }
        }
    }

    private void BagController_OnAddNewBagHandler(BagController.BagData bagData)
    {
        var item = CreateBagItem();
        item.SetData(bagData);
        _items.Add(item);
    }

    private void LoadBags()
    {
        var bags = BagController.Instance.GetBags();
        foreach (var bagData in bags)
        {
            var item = CreateBagItem();
            item.SetData(bagData);
            _items.Add(item);
        }
    }

    private BagBoxItem CreateBagItem()
    {
        var clone = _baseBagBoxItem.Clone();
        clone.OnClickSelectBagItemHandler = OnClickSelectBagItemHandler;
        clone.OnClickSellItemHandler = OnClickSellItemHandler;
        return clone;
    }

    private void OnClickSellItemHandler(BagBoxItem item)
    {
        var shopData = ShopAvatarController.Instance.FindShopDataByAvatarId(item.BagData.avatarId);
        ConfirmPopup.ShowPopup(LanguageConst.CONFIRM_SELL_AVATAR_TITLE, LanguageConst.CONFIRM_SELL_AVATAR_CONTENT.Replace("[COIN]", shopData.sell.ToString()), ok =>
        {
            if (ok)
            {
                ShopAvatarController.Instance.Sell(shopData, () =>
                {
                    MessagePopup.ShowPopup(LanguageConst.MESSAGE_TITLE, LanguageConst.MESSAGE_SELL_ITEM_COMPLETE_CONTENT);
                });
            }
        });
    }

    private void OnClickSelectBagItemHandler(BagBoxItem item)
    {
        if (_selectedItem == item)
        {
            ConfirmPopup.ShowPopup(LanguageConst.CONFIRM_EQUIP_AVATAR_TITLE, LanguageConst.CONFIRM_EQUIP_AVATAR_CONTENT, ok =>
            {
                if (ok)
                {
                    AvatarController.Instance.EquipAvatar(item.BagData.avatarType, item.BagData.avatarId);
                }
            });
            return;
        }

        if (_selectedItem != null)
        {
            _selectedItem.UnSelected();
        }
        _selectedItem = item;

        _selectedItem.Selected();
    }
}
