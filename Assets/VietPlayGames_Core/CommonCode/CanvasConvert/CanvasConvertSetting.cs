using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasConvertSetting : ScriptableObject
{
    public const string SETTING_PATH = "CanvasConvertSetting";
    private static CanvasConvertSetting _instance;
    public static CanvasConvertSetting Instance
    {
        get
        {
            if (ReferenceEquals(_instance, null))
            {
                _instance = Resources.Load(SETTING_PATH) as CanvasConvertSetting;
                if (ReferenceEquals(_instance, null))
                {
                    throw new System.Exception("CanvasConvertSetting not found!!!");
                }
            }
            return _instance;
        }
    }

    public Vector2 SizeTablet = new Vector2(1000, 1000);
    public Vector2 SizeMobile = new Vector2(750, 750);
    public float DPI = 160.0f;
    public float EDITOR_DPI = 380f;
}
