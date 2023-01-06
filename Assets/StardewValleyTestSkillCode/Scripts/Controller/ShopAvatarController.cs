using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAvatarController : MonoBehaviour
{
    public static ShopAvatarController Instance = new ShopAvatarController();
    private Dictionary<AvatarType, List<ShopAvatarData>> _shops = new Dictionary<AvatarType, List<ShopAvatarData>>();

    public ShopAvatarController()
    {
        //config
        _shops.Add(AvatarType.Hat, new List<ShopAvatarData>());
        _shops[AvatarType.Hat].Add(new ShopAvatarData() { id = "hat_1", avatarType = AvatarType.Hat, price = 10 });
        _shops[AvatarType.Hat].Add(new ShopAvatarData() { id = "hat_2", avatarType = AvatarType.Hat, price = 10 });
        _shops[AvatarType.Hat].Add(new ShopAvatarData() { id = "hat_3", avatarType = AvatarType.Hat, price = 10 });
        _shops[AvatarType.Hat].Add(new ShopAvatarData() { id = "hat_4", avatarType = AvatarType.Hat, price = 10 });
        _shops[AvatarType.Hat].Add(new ShopAvatarData() { id = "hat_5", avatarType = AvatarType.Hat, price = 10 });

        _shops.Add(AvatarType.Cloth, new List<ShopAvatarData>());
        _shops[AvatarType.Cloth].Add(new ShopAvatarData() { id = "cloth_1", avatarType = AvatarType.Cloth, price = 10 });
        _shops[AvatarType.Cloth].Add(new ShopAvatarData() { id = "cloth_2", avatarType = AvatarType.Cloth, price = 10 });
        _shops[AvatarType.Cloth].Add(new ShopAvatarData() { id = "cloth_3", avatarType = AvatarType.Cloth, price = 10 });
        _shops[AvatarType.Cloth].Add(new ShopAvatarData() { id = "cloth_4", avatarType = AvatarType.Cloth, price = 10 });
        _shops[AvatarType.Cloth].Add(new ShopAvatarData() { id = "cloth_5", avatarType = AvatarType.Cloth, price = 10 });
    }
}
