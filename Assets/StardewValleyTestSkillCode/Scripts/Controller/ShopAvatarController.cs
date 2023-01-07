using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAvatarController
{
    public static ShopAvatarController Instance = new ShopAvatarController();
    private Dictionary<AvatarType, List<ShopAvatarData>> _shops = new Dictionary<AvatarType, List<ShopAvatarData>>();

    public ShopAvatarController()
    {
        //config
        _shops.Add(AvatarType.Hat, new List<ShopAvatarData>());
        _shops[AvatarType.Hat].Add(new ShopAvatarData() { id = "hat_1", name = "Hat 1", avatarType = AvatarType.Hat, price = 10 });
        _shops[AvatarType.Hat].Add(new ShopAvatarData() { id = "hat_2", name = "Hat 2", avatarType = AvatarType.Hat, price = 10 });
        _shops[AvatarType.Hat].Add(new ShopAvatarData() { id = "hat_3", name = "Hat 3", avatarType = AvatarType.Hat, price = 10 });
        _shops[AvatarType.Hat].Add(new ShopAvatarData() { id = "hat_4", name = "Hat 4", avatarType = AvatarType.Hat, price = 10 });
        _shops[AvatarType.Hat].Add(new ShopAvatarData() { id = "hat_5", name = "Hat 5", avatarType = AvatarType.Hat, price = 10 });

        _shops.Add(AvatarType.Cloth, new List<ShopAvatarData>());
        _shops[AvatarType.Cloth].Add(new ShopAvatarData() { id = "cloth_1", name = "Cloth 1", avatarType = AvatarType.Cloth, price = 10 });
        _shops[AvatarType.Cloth].Add(new ShopAvatarData() { id = "cloth_2", name = "Cloth 2", avatarType = AvatarType.Cloth, price = 10 });
        _shops[AvatarType.Cloth].Add(new ShopAvatarData() { id = "cloth_3", name = "Cloth 3", avatarType = AvatarType.Cloth, price = 10 });
        _shops[AvatarType.Cloth].Add(new ShopAvatarData() { id = "cloth_4", name = "Cloth 4", avatarType = AvatarType.Cloth, price = 10 });
        _shops[AvatarType.Cloth].Add(new ShopAvatarData() { id = "cloth_5", name = "Cloth 5", avatarType = AvatarType.Cloth, price = 10 });
    }

    public List<ShopAvatarData> FindShopByAvatarType(AvatarType avatarType)
    {
        if (_shops.ContainsKey(avatarType))
        {
            return _shops[avatarType];
        }

        return null;
    }

    public GameObject LoadAvatarIcon(AvatarType avatarType,string id)
    {
        var re = Resources.Load(string.Format("Avatars/Icons/{0}/icon_{1}", avatarType, id));
        if (re == null)
        {
            re = Resources.Load("Avatars/Icons/icon_unknown");
        }
        return GameObject.Instantiate((GameObject)re);
    }
}
