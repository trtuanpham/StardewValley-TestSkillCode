using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public static class UnityUtils
{
    public static void CopyToClipboard(this string s)
    {
        TextEditor te = new TextEditor();
        te.text = s;
        te.SelectAll();
        te.Copy();
    }

    public static T CheckResources<T>(string path) where T : MonoBehaviour
    {
        return Resources.Load<T>(path);
    }

    public static T SpawnResources<T>(string path) where T : MonoBehaviour
    {
        var baseObject = Resources.Load<T>(path);
        if (baseObject != null)
        {
            return GameObject.Instantiate(baseObject);
        }
        return null;
    }

    public static GameObject SpawnResources(string path)
    {
        var baseObject = Resources.Load(path) as GameObject;
        if (baseObject != null)
        {
            var clone = GameObject.Instantiate(baseObject);
            return clone;
        }

        return null;
    }

    public static Sprite SpriteResources(string path)
    {
        var baseObject = Resources.Load<Sprite>(path);
        return baseObject;
    }

    public static void ResetTransform(this Transform trans)
    {
        trans.localScale = Vector3.one;
        trans.localPosition = Vector3.zero;
        trans.localRotation = Quaternion.identity;
    }

    public static void ClearChildTransform(this Transform trans)
    {
        foreach (Transform child in trans)
        {
            UnityEngine.Object.Destroy(child.gameObject);
        }
    }

    public static void DestroyImmediateChildTransform(this Transform trans)
    {
        foreach (Transform child in trans)
        {
            UnityEngine.Object.DestroyImmediate(child.gameObject);
        }
    }

    public static void SetParent(this MonoBehaviour source, GameObject target)
    {
        source.transform.SetParent(target.transform);
    }

    public static void SetParent(this MonoBehaviour source, MonoBehaviour target)
    {
        source.transform.SetParent(target.transform);
    }

    public static void SetParent(this MonoBehaviour source, Transform target)
    {
        source.transform.SetParent(target);
    }

    public static void RunDelay(this MonoBehaviour monoBehaviour, Action callback, float delay = 0.5f)
    {
        monoBehaviour.StartCoroutine(IE_Rundelay(callback, delay));
    }

    public static void RunIEnumerator(this MonoBehaviour monoBehaviour, IEnumerator enumerator)
    {
        monoBehaviour.StartCoroutine(enumerator);
    }

    public static bool MonoAlive(this MonoBehaviour monoBehaviour)
    {
        return monoBehaviour != null && monoBehaviour.gameObject != null;
    }

    private static IEnumerator IE_Rundelay(Action callback, float delay)
    {
        if (delay <= 0)
        {
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(delay);
        }

        callback?.Invoke();
    }

    public static void CaptureScreenshot(this MonoBehaviour monoBehaviour, Action<Texture2D> callback)
    {
        monoBehaviour.StartCoroutine(IE_CaptureScreenshot(callback));
    }

    private static IEnumerator IE_CaptureScreenshot(Action<Texture2D> callback)
    {
        yield return new WaitForEndOfFrame();
        var texture2D = ScreenCapture.CaptureScreenshotAsTexture();
        callback?.Invoke(texture2D);
    }

    public static void DisableObject(this MonoBehaviour mono)
    {
        mono.gameObject.SetActive(false);
    }

    public static void DisableObject(this GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public static void EnableObject(this GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public static void EnableObject(this MonoBehaviour mono)
    {
        mono.gameObject.SetActive(true);
    }

    public static T Clone<T>(this T baseMono, Transform parent = null) where T : MonoBehaviour
    {
        if (parent == null)
        {
            parent = baseMono.gameObject.transform.parent;
        }
        var clone = UnityEngine.Object.Instantiate(baseMono, parent);

        clone.transform.localScale = baseMono.transform.localScale;
        clone.transform.localPosition = Vector3.zero;

        clone.EnableObject();

        return clone;
    }

    public static GameObject Clone(this GameObject gameObject, Transform parent = null)
    {
        if (parent == null)
        {
            parent = gameObject.transform.parent;
        }
        var clone = UnityEngine.Object.Instantiate(gameObject, parent);

        clone.transform.localScale = gameObject.transform.localScale;
        clone.transform.localPosition = Vector3.zero;
        clone.EnableObject();

        return clone;
    }

    public static string Debug(this Hashtable hashtable)
    {
        return JsonConvert.SerializeObject(hashtable);
    }
}