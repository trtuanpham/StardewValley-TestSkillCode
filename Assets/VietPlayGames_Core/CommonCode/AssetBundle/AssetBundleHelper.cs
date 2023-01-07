using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleHelper : Singleton<AssetBundleHelper>
{
    private Dictionary<string, AssetBundle> _assetBundles = new Dictionary<string, AssetBundle>();

    public void UnloadAllAssetBundle()
    {
        AssetBundle.UnloadAllAssetBundles(true);
    }

    public AssetBundle GetBunder(string bundleName)
    {
        if (_assetBundles.ContainsKey(bundleName))
        {
            return _assetBundles[bundleName];
        }
        return null;
    }

    public bool Load(BundleData bundleData, Action callback = null, Action<string> errorCallback = null)
    {
       return AssetBundleHelper.Instance.CheckLoadDownloadAssetBundle(bundleData, assetBundle =>
        {
            if (_assetBundles.ContainsKey(bundleData.bundleName))
            {
                _assetBundles[bundleData.bundleName] = assetBundle;
            }
            else
            {
                _assetBundles.Add(bundleData.bundleName, assetBundle);
            }
            callback?.Invoke();
        }, errorCallback);
    }

    private bool CheckLoadDownloadAssetBundle(BundleData bundle, Action<AssetBundle> callback = null, Action<string> errorCallback = null)
    {
        var version = PlayerPrefs.GetString(bundle.bundleName + "_assetbundle_version", "0");
        if (version != bundle.version)
        {
            PlayerPrefs.SetString(bundle.bundleName + "_assetbundle_version", bundle.version);
            PlayerPrefs.Save();
            DownloadBundle(bundle.bundleName, bundle.url, borderAssetBundle =>
            {
                callback?.Invoke(borderAssetBundle);
            }, errorCallback);
            return true;
        }
        else
        {
            if (CheckExistsLocalAssetBundle(bundle.bundleName))
            {
                LoadAssetBundleLocal(bundle.bundleName, assetBundle =>
                {
                    callback?.Invoke(assetBundle);
                }, error =>
                {
                    Debug.LogError("Load assetbundle on local error: " + error);
                    errorCallback?.Invoke(error);
                });
            }
            else
            {
                DownloadBundle(bundle.bundleName, bundle.url, borderAssetBundle =>
                {
                    callback?.Invoke(borderAssetBundle);
                }, errorCallback);
            }
            return false;
        }
    }

    public bool CheckVersionAssetBundle(BundleData bundle)
    {
        var version = PlayerPrefs.GetString(bundle.bundleName + "_assetbundle_version", "0");
        if (version != bundle.version)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DownloadBundle(string assetBundleName, string url, Action<AssetBundle> callback = null, Action<string> errorCallback = null)
    {
        StartCoroutine(IE_DownloadBundle(assetBundleName, url, callback, errorCallback));
    }

    private IEnumerator IE_DownloadBundle(string assetBundleName, string url, Action<AssetBundle> callback, Action<string> errorCallback)
    {
        Debug.Log("Download assetbundle: " + url);
        UnityWebRequest www = UnityWebRequest.Get(url);

        //Send Request and wait
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
            errorCallback?.Invoke(www.error);
        }
        else
        {
            SaveAssetBundle(www.downloadHandler.data, assetBundleName);
            LoadAssetBundleLocal(assetBundleName, callback, errorCallback);

            Debug.Log("Downloaded assetbundle: " + assetBundleName);
        }
    }

    public void LoadAssetBundleLocal(string assetBundleName, Action<AssetBundle> callback = null, Action<string> errorCallback = null)
    {
        StartCoroutine(IE_LoadAssetBundleLocal(assetBundleName, callback, errorCallback));
    }

    IEnumerator IE_LoadAssetBundleLocal(string assetBundleName, Action<AssetBundle> callback = null, Action<string> errorCallback = null)
    {
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.temporaryCachePath, assetBundleName + ".assetbundle"));
        yield return bundleLoadRequest;

        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            errorCallback?.Invoke("Failed to load AssetBundle!");
            yield break;
        }
        callback?.Invoke(myLoadedAssetBundle);
    }

    private void SaveAssetBundle(byte[] data, string assetBundleName)
    {
        string cachedAssetBundle = Application.temporaryCachePath + "/" + assetBundleName + ".assetbundle";
        System.IO.FileStream cache = new System.IO.FileStream(cachedAssetBundle, System.IO.FileMode.Create);
        cache.Write(data, 0, data.Length);
        cache.Flush();
        cache.Close();

#if UNITY_IOS
        UnityEngine.iOS.Device.SetNoBackupFlag(cachedAssetBundle);
#endif
    }

    private bool CheckExistsLocalAssetBundle(string assetBundleName)
    {
        string cachedAssetBundle = Application.temporaryCachePath + "/" + assetBundleName + ".assetbundle";
        return File.Exists(cachedAssetBundle);
    }

    public T GetAssetBundleItem<T>(AssetBundle assetBundle, string itemName) where T : MonoBehaviour
    {
        T go = Instantiate(assetBundle.LoadAsset<T>(itemName));
        return go;
    }

    public GameObject GetAssetBundleItem(AssetBundle assetBundle, string itemName)
    {
        GameObject go = Instantiate(assetBundle.LoadAsset<GameObject>(itemName));
        return go;
    }
}

public class BundleData
{
    public string bundleName;
    public string version;
    public string androidUrl;
    public string iOSUrl;

    public string url
    {
        get
        {
#if UNITY_ANDROID
                return androidUrl;
#elif UNITY_IOS
            return iOSUrl;
#else
                Debug.LogWarning("need to update!!!");
                return string.Empty;
#endif
        }
    }
}
