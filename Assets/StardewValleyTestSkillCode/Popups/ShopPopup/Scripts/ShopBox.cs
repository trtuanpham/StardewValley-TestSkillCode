using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBox : BaseMonoBehaviour
{
    [SerializeField] AvatarType _avatarType;
    [SerializeField] ShopBoxItem _baseShopBoxItem;

    private ShopBoxItem _selectedItem;
    private List<ShopBoxItem> _items = new List<ShopBoxItem>();

    private void Awake()
    {
        _baseShopBoxItem.gameObject.SetActive(false);
    }

    protected override void OnInit()
    {
        base.OnInit();

        LoadShop();
    }

    private void LoadShop()
    {
        var shops = ShopAvatarController.Instance.FindShopByAvatarType(_avatarType);
        foreach (var shopAvatarData in shops)
        {
            var item = CreateShopBoxItem();
            item.SetData(shopAvatarData);
            _items.Add(item);
        }
    }

    private ShopBoxItem CreateShopBoxItem()
    {
        var clone = _baseShopBoxItem.Clone();
        clone.OnClickSelectShopItemHandler = OnClickSelectShopItemHandler;
        return clone;
    }

    private void OnClickSelectShopItemHandler(ShopBoxItem item)
    {
        if (_selectedItem == item)
        {

            ConfirmPopup.ShowPopup(LanguageConst.CONFIRM_BUY_AVATAR_TITLE, LanguageConst.CONFIRM_BUY_AVATAR_CONTENT, ok =>
            {
                if (ok)
                {
                    ShopAvatarController.Instance.Buy(item.shopAvatarData, () =>
                    {

                    }, error =>
                    {
                        if (error == "no_enough_coin")
                        {
                            MessagePopup.ShowPopup(LanguageConst.MESSAGE_BUY_TITLE, LanguageConst.MESSAGE_ERROR_NO_ENOUGH_COIN_CONTENT);
                        }
                    });
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
