using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsChecker
{
    public static readonly PlayerPrefsChecker Instance = new PlayerPrefsChecker();
#if UNITY_EDITOR
    private List<string> _keys;
    private List<string> _checkedKeys;
    public PlayerPrefsChecker()
    {
        _keys = new List<string>();
        _checkedKeys = new List<string>();
    }

    public void TestKey(string key)
    {

        if (_checkedKeys.Contains(key))
        {
            return;
        }

        if (!_keys.Contains(key))
        {
            _keys.Add(key);
            _checkedKeys.Add(key);
            return;
        }
        Debug.LogError(string.Format("Key {0} was existed", key));
    }
#else
    public PlayerPrefsChecker(){}
    public void TestKey(string key){}
#endif
}