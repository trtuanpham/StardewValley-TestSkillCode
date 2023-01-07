using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DefineEditor
{
    public static void AddDefine(string defineKey)
    {
        //android
#if UNITY_ANDROID
        var define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        if (!define.Contains(defineKey))
        {
            if (define.Length > 0)
            {
                define += ";" + defineKey;
            }
            else
            {
                define = defineKey;
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, define);
        }

#elif UNITY_IOS
        var define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
        if (!define.Contains(defineKey))
        {
            if (define.Length > 0)
            {
                define += ";" + defineKey;
            }
            else
            {
                define = defineKey;
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, define);
        }
#endif
    }

    public static void RemoveDefine(string defineKey)
    {
#if UNITY_ANDROID
        var define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        if (define.Contains(defineKey))
        {
            define = define.Replace(defineKey, "");
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, define);
        }
#elif UNITY_IOS
        var define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
        if (define.Contains(defineKey))
        {
            define = define.Replace(defineKey, "");
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, define);
        }
#endif
    }

    public static bool ContainKey(string defineKey)
    {
#if UNITY_ANDROID
        var define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        return define.Contains(defineKey);
#elif UNITY_IOS
        var define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
        return define.Contains(defineKey);
#else
        return false;
#endif

    }
}