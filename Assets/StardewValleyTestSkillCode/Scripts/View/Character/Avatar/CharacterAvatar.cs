using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAvatar : MonoBehaviour
{
    [SerializeField] Transform _hatPlace;
    [SerializeField] Transform _clothPlace;

    private CharacterAvatarItem _hatItem;
    private CharacterAvatarItem _clothItem;


    public void Equip(AvatarType avatarType, string avatarId)
    {

    }
}
