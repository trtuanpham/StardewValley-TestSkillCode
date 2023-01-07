using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAvatarController
{
    public static ShopAvatarController Instance = new ShopAvatarController();
    private Dictionary<string, ShopAvatarData> _shops = new Dictionary<string, ShopAvatarData>();
    private Dictionary<AvatarType, List<ShopAvatarData>> _shopByTypes = new Dictionary<AvatarType, List<ShopAvatarData>>();

    public ShopAvatarController()
    {
        //config
        _shops.Add("hat_1", new ShopAvatarData() { id = "hat_1", name = "Hat 1", avatarType = AvatarType.Hat, price = 10, sell = 5 });
        _shops.Add("hat_2", new ShopAvatarData() { id = "hat_2", name = "Hat 2", avatarType = AvatarType.Hat, price = 20, sell = 10 });
        _shops.Add("hat_3", new ShopAvatarData() { id = "hat_3", name = "Hat 3", avatarType = AvatarType.Hat, price = 50, sell = 25 });
        _shops.Add("hat_4", new ShopAvatarData() { id = "hat_4", name = "Hat 4", avatarType = AvatarType.Hat, price = 100, sell = 50 });
        _shops.Add("cloth_1", new ShopAvatarData() { id = "cloth_1", name = "Cloth 1", avatarType = AvatarType.Cloth, price = 10, sell = 5 });
        _shops.Add("cloth_2", new ShopAvatarData() { id = "cloth_2", name = "Cloth 2", avatarType = AvatarType.Cloth, price = 20, sell = 10 });
        _shops.Add("cloth_3", new ShopAvatarData() { id = "cloth_3", name = "Cloth 3", avatarType = AvatarType.Cloth, price = 50, sell = 20 });
        _shops.Add("cloth_4", new ShopAvatarData() { id = "cloth_4", name = "Cloth 4", avatarType = AvatarType.Cloth, price = 100, sell = 50 });
        _shops.Add("cloth_5", new ShopAvatarData() { id = "cloth_5", name = "Cloth 5", avatarType = AvatarType.Cloth, price = 150, sell = 100 });

        _shopByTypes = new Dictionary<AvatarType, List<ShopAvatarData>>();
        _shopByTypes[AvatarType.Hat] = new List<ShopAvatarData>();
        _shopByTypes[AvatarType.Cloth] = new List<ShopAvatarData>();
        foreach (var shopData in _shops.Values)
        {
            _shopByTypes[shopData.avatarType].Add(shopData);
        }
    }

    public List<ShopAvatarData> FindShopByAvatarType(AvatarType avatarType)
    {
        if (_shopByTypes.ContainsKey(avatarType))
        {
            return _shopByTypes[avatarType];
        }

        return null;
    }

    public void Buy(ShopAvatarData shopAvatarData, Action callback = null, Action<string> errorCallback = null)
    {
        if (shopAvatarData.price > PlayerController.Instance.coin)
        {
            errorCallback?.Invoke("no_enough_coin");
            return;
        }

        PlayerController.Instance.UpdateCoin(-shopAvatarData.price);

        BagController.Instance.AddBag(shopAvatarData.id, shopAvatarData.avatarType);

        callback?.Invoke();
    }

    public void Sell(ShopAvatarData shopAvatarData, Action callback = null, Action<string> errorCallback = null)
    {
        PlayerController.Instance.UpdateCoin(shopAvatarData.sell);
        BagController.Instance.RemoveBag(shopAvatarData.id, shopAvatarData.avatarType);

        callback?.Invoke();
    }

    public ShopAvatarData FindShopDataByAvatarId(string avatarId)
    {
        if (_shops.ContainsKey(avatarId))
        {
            return _shops[avatarId];
        }

        return null;
    }

    public GameObject LoadAvatarIcon(AvatarType avatarType, string id)
    {
        var re = Resources.Load(string.Format("Avatars/Icons/{0}/icon_{1}", avatarType, id));
        if (re == null)
        {
            re = Resources.Load("Avatars/Icons/icon_unknown");
        }
        return GameObject.Instantiate((GameObject)re);
    }
}
