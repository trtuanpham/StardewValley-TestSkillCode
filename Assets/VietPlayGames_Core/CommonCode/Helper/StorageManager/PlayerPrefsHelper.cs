using System;
using Newtonsoft.Json;
using UnityEngine;

public static class PlayerPrefsHelper
{
    //save and load bool
    public static bool LoadBool(string key, bool defaultValue = false)
    {
        PlayerPrefsChecker.Instance.TestKey(key);
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }

    public static void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    //save and load string
    public static string LoadString(string key, string defaultValue = "")
    {
        PlayerPrefsChecker.Instance.TestKey(key);
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public static void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    //save and load float
    public static float LoadFloat(string key, float defaultValue = 0)
    {
        PlayerPrefsChecker.Instance.TestKey(key);
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public static void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    //save and load int
    public static int LoadInt(string key, int defaultValue = 0)
    {
        PlayerPrefsChecker.Instance.TestKey(key);
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public static void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    //save and load int
    public static long LoadLong(string key, long defaultValue = 0)
    {
        PlayerPrefsChecker.Instance.TestKey(key);
        var sLong = PlayerPrefs.GetString(key, "0");
        return long.Parse(sLong);
    }

    public static void SaveLong(string key, long value)
    {
        PlayerPrefs.SetString(key, value.ToString());
        PlayerPrefs.Save();
    }

    //json
    public static T LoadJson<T>(string key)
    {
        PlayerPrefsChecker.Instance.TestKey(key);
        var json = PlayerPrefs.GetString(key, "");
        if (string.IsNullOrEmpty(json))
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        return JsonConvert.DeserializeObject<T>(json);
    }

    public static void SaveJson(this object value, string key)
    {
        var json = JsonConvert.SerializeObject(value);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    //remove
    public static void Remove(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public static bool Exist(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public static void RemoveAll()
    {
        PlayerPrefs.DeleteAll();
    }
}