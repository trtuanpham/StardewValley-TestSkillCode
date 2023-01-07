using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class IAPEditor : MonoBehaviour
{
    private const string IAP_DEFINE = "IAP";
    private const string USE_PAYOUTS = "USE_PAYOUTS";
    private const string RECEIPT_VALIDATION = "RECEIPT_VALIDATION";

    private const string LINK_MENU = "VietPlayGames/IAP";

    private static bool _hasIAP;

    static IAPEditor()
    {
        _hasIAP = DefineEditor.ContainKey(IAP_DEFINE);
        EditorApplication.delayCall += () => {
            OnCheckMark();
        };
    }

    [MenuItem(LINK_MENU)]
    private static void AddIAP()
    {
        _hasIAP = !_hasIAP;

        if (!_hasIAP)
        {
            DefineEditor.RemoveDefine(IAP_DEFINE);
        }
        else
        {
            DefineEditor.AddDefine(IAP_DEFINE);
        }

        OnCheckMark();
    }

    private static void OnCheckMark()
    {
        Menu.SetChecked(LINK_MENU, _hasIAP);
    }
}