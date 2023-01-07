using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagBoxItem : BaseMonoBehaviour
{
    [SerializeField] Transform _iconPlace;
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] ValueBox _numberBox;

    [SerializeField] GameObject _selectObject;
    [SerializeField] Button _selectButton;
    [SerializeField] Button _sellButton;

    [SerializeField] GameObject _equipObject;

    public Action<BagBoxItem> OnClickSelectBagItemHandler;
    public Action<BagBoxItem> OnClickSellItemHandler;

    private BagController.BagData _bagData;

    public BagController.BagData BagData
    {
        get
        {
            return _bagData;
        }
    }

    private void Awake()
    {
        _selectObject.SetActive(false);
    }

    protected override void OnAddEvent()
    {
        base.OnAddEvent();

        _sellButton.onClick.AddListener(OnClickSellButtonHandler);
        _selectButton.onClick.AddListener(OnClickSelectButtonHandler);
        AvatarController.Instance.OnEquipAvatarHandler += AvatarController_OnEquipAvatarHandler;
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();

        _sellButton.onClick.RemoveListener(OnClickSellButtonHandler);
        _selectButton.onClick.RemoveListener(OnClickSelectButtonHandler);
        AvatarController.Instance.OnEquipAvatarHandler -= AvatarController_OnEquipAvatarHandler;
    }

    private void AvatarController_OnEquipAvatarHandler(AvatarType avatarType, string avatarId)
    {
        _equipObject.SetActive(AvatarController.Instance.IsEquip(_bagData.avatarType, _bagData.avatarId));
    }

    private void OnClickSelectButtonHandler()
    {
        OnClickSelectBagItemHandler?.Invoke(this);
    }

    private void OnClickSellButtonHandler()
    {
        OnClickSellItemHandler?.Invoke(this);
    }

    public void SetData(BagController.BagData bagData)
    {
        _bagData = bagData;
       var shopData = ShopAvatarController.Instance.FindShopDataByAvatarId(_bagData.avatarId);
        _nameText.text = shopData.name;
        _numberBox.SetData(bagData.number);
        var icon = ShopAvatarController.Instance.LoadAvatarIcon(shopData.avatarType, shopData.id);
        if (icon != null)
        {
            icon.transform.SetParent(_iconPlace);
            icon.transform.ResetTransform();
        }

        _equipObject.SetActive(AvatarController.Instance.IsEquip(_bagData.avatarType, _bagData.avatarId));
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
