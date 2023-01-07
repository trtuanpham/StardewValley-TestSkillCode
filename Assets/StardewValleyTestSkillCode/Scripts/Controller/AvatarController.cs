using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController
{
    public static AvatarController Instance = new AvatarController();

    public AvatarData Avatar { get; private set; }

    public AvatarController()
    {
        Avatar = new AvatarData();
        Avatar.hat = "hat_0";
        Avatar.cloth = "cloth_0";
    }

    public event Action<AvatarType, string> OnEquipAvatarHandler;

    public void EquipAvatar(AvatarType avatarType, string avatarId)
    {
        switch (avatarType)
        {
            case AvatarType.Hat:
                Avatar.hat = avatarId;
                break;

            case AvatarType.Cloth:
                Avatar.cloth = avatarId;
                break;
            default:
                break;
        }

        OnEquipAvatarHandler?.Invoke(avatarType, avatarId);
    }

    public void UnEquipAvatar(AvatarType avatarType, string avatarId)
    {
        switch (avatarType)
        {
            case AvatarType.Hat:
                if(Avatar.hat == avatarId)
                {
                    Avatar.hat = string.Empty;
                }
                break;

            case AvatarType.Cloth:
                if (Avatar.cloth == avatarId)
                {
                    Avatar.cloth = string.Empty;
                }
                break;
            default:
                break;
        }

        OnEquipAvatarHandler?.Invoke(avatarType, avatarId);
    }

    public bool IsEquip(AvatarType avatarType, string avatarId)
    {
        switch (avatarType)
        {
            case AvatarType.Hat:
                if (Avatar.hat == avatarId)
                {
                    return true;
                }
                break;

            case AvatarType.Cloth:
                if (Avatar.cloth == avatarId)
                {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }
}
