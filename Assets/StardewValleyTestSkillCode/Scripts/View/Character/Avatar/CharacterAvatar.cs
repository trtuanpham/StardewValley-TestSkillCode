using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAvatar : MonoBehaviour
{
    [SerializeField] Transform _hatPlace;
    [SerializeField] Transform _clothPlace;

    private GameObject _hatItem;
    private GameObject _clothItem;

    private AvatarData _avatarData;

    public void SetData(AvatarData avatarData)
    {
        _avatarData = avatarData;
        Equip(AvatarType.Hat, _avatarData.hat);
        Equip(AvatarType.Cloth, _avatarData.cloth);
    }

    public void Equip(AvatarType avatarType, string avatarId)
    {
        switch (avatarType)
        {
            case AvatarType.Hat:
                _avatarData.hat = avatarId;
                if (_hatItem != null)
                {
                    Destroy(_hatItem.gameObject);
                }
                _hatItem = LoadAvatar(avatarType, avatarId);
                if (_hatItem != null)
                {
                    _hatItem.transform.SetParent(_hatPlace);
                    _hatItem.transform.ResetTransform();
                }
                break;
            case AvatarType.Cloth:
                _avatarData.cloth = avatarId;
                if (_clothItem != null)
                {
                    Destroy(_clothItem.gameObject);
                }
                _clothItem = LoadAvatar(avatarType, avatarId);
                if (_clothItem != null)
                {
                    _clothItem.transform.SetParent(_clothPlace);
                    _clothItem.transform.ResetTransform();
                }
                break;
        }
    }

    public GameObject LoadAvatar(AvatarType avatarType, string id)
    {
        var re = Resources.Load<GameObject>(string.Format("Avatars/{0}/{1}", avatarType, id));
        if (re != null)
        {
            return GameObject.Instantiate(re);
        }
        return null;
    }
}
