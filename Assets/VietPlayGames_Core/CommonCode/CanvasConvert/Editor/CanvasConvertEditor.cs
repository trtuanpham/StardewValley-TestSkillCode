using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CanvasConvertEditor : Editor
{
    public const string PATH = "Assets/Resources/CanvasConvertSetting.asset";

    [MenuItem("Tools/Canvas convert setting")]
    public static void OpenSetting()
    {
        CanvasConvertSetting setting = Resources.Load(CanvasConvertSetting.SETTING_PATH) as CanvasConvertSetting;

        if(setting == null)
        {
            setting = CreateInstance<CanvasConvertSetting>();
            AssetDatabase.CreateAsset(setting, PATH);
            AssetDatabase.SaveAssets();
            setting = Resources.Load(CanvasConvertSetting.SETTING_PATH) as CanvasConvertSetting;
        }

        Selection.activeObject = setting;
    }
}
