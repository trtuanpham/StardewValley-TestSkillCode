
using UnityEngine;

public static class DeviceDetect
{
//#if UNITY_IOS
//    public static bool IsIpad()
//    {
//        return UnityEngine.iOS.Device.generation.ToString().Contains("iPad");
//    }
//#endif

    public static bool IsTabletResolution()
    {
        var width = Screen.width;
        var height = Screen.height;
        var aspect = (width > height) ? (float)width / (float)height : (float)height / (float)width;
        //Debug.LogError(width + "/" + height);
        Debug.Log("aspect: " + aspect);
        return aspect <= 1.5f;
    }
}
