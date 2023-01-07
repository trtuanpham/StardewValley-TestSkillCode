using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BagController
{
    public static BagController Instance = new BagController();

    private Dictionary<string, BagData> _bags = new Dictionary<string, BagData>();

    public event Action<BagData> OnAddNewBagHandler;
    public event Action<BagData> OnUpdateBagHandler;
    public event Action<AvatarType, string> OnRemoveBagHandler;

    public IList<BagData> GetBags()
    {
        return _bags.Values.ToList().AsReadOnly();
    }

    public void AddBag(string avatarId, AvatarType avatarType)
    {
        if (_bags.ContainsKey(avatarId))
        {
            _bags[avatarId].number++;
            OnUpdateBagHandler?.Invoke(_bags[avatarId]);
        }
        else
        {
            _bags.Add(avatarId, new BagData() { avatarId = avatarId, avatarType = avatarType, number = 1 });
            OnAddNewBagHandler?.Invoke(_bags[avatarId]);
        }
    }

    public void RemoveBag(string avatarId, AvatarType avatarType)
    {
        if (_bags.ContainsKey(avatarId))
        {
            if (_bags[avatarId].number > 1)
            {
                _bags[avatarId].number--;
                OnUpdateBagHandler?.Invoke(_bags[avatarId]);
            }
            else
            {
                _bags.Remove(avatarId);
                OnRemoveBagHandler?.Invoke(avatarType, avatarId);
            }
        }
    }

    public class BagData
    {
        public string avatarId;
        public AvatarType avatarType;
        public int number;
    }
}
