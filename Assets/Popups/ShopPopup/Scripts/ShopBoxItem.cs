using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBoxItem : BaseMonoBehaviour
{
    [SerializeField] Transform _iconPlace;
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] ValueBox _priceBox;

    [SerializeField] GameObject _selectObject;
    [SerializeField] Button _selectButton;
    //[SerializeField] Button _buyButton;

    public Action<ShopBoxItem> OnClickSelectShopItemHandler;
    public Action<ShopBoxItem> OnClickBuyShopItemHandler;

    private ShopAvatarData _shopAvatarData;

    public ShopAvatarData shopAvatarData
    {
        get
        {
            return _shopAvatarData;
        }
    }

    private void Awake()
    {
        _selectObject.SetActive(false);
    }

    protected override void OnAddEvent()
    {
        base.OnAddEvent();

        //_buyButton.onClick.AddListener(OnClickBuyButtonHandler);
        _selectButton.onClick.AddListener(OnClickSelectButtonHandler);
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();

        //_buyButton.onClick.RemoveListener(OnClickBuyButtonHandler);
        _selectButton.onClick.RemoveListener(OnClickSelectButtonHandler);
    }

    private void OnClickSelectButtonHandler()
    {
        OnClickSelectShopItemHandler?.Invoke(this);
    }

    private void OnClickBuyButtonHandler()
    {
        
    }

    public void SetData(ShopAvatarData data)
    {
        _shopAvatarData = data;
        _nameText.text = _shopAvatarData.name;
        _priceBox.SetData(_shopAvatarData.price);
        var icon = ShopAvatarController.Instance.LoadAvatarIcon(data.avatarType, data.id);
        if (icon != null)
        {
            icon.transform.SetParent(_iconPlace);
            icon.transform.ResetTransform();
        }
    }

    public void Selected()
    {
        _selectObject.SetActive(true);
    }

    public void UnSelected()
    {
        _selectObject.SetActive(false);
    }
}
