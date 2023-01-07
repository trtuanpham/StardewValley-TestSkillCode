using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Swing.Editor;
using UnityEditor;
using UnityEngine;

public class PopupEditor : EditorWindow
{
    [SerializeField] string _popupClassName = "NoNamePopup";
    [SerializeField] string _path = "Assets/Popups";

    private GameObject _tempPopup;

    public static string RootPath
    {
        get
        {
            var g = AssetDatabase.FindAssets("t:Script PopupEditor");
            return AssetDatabase.GUIDToAssetPath(g[0]);
        }
    }

    // Add menu named "My Window" to the Window menu
    [MenuItem("VietPlayGames/Popup/AddNewPopup")]
    static void Init()
    {
        //_tempPopup = 
        // Get existing open window or if none, make a new one:
        PopupEditor window = (PopupEditor)EditorWindow.GetWindow(typeof(PopupEditor));
        window.maxSize = new Vector2(300, 200);
        window.Show();
        string path = GetParent(GetParent(RootPath));
        window._tempPopup = AssetDatabase.LoadAssetAtPath<GameObject>(path + "/Prefabs/TempPopup.prefab");
        //Debug.Log(path);
    }

    static string GetParent(string path)
    {
        return path.Substring(0, path.LastIndexOf("/"));
    }

    void OnGUI()
    {
        GUILayout.Label("New Popup", EditorStyles.boldLabel);
        _popupClassName = EditorGUILayout.TextField("Popup class name:", _popupClassName);
        _path = EditorGUILayout.TextField("Path:", _path);
        _tempPopup = (GameObject)EditorGUILayout.ObjectField("Script Location", _tempPopup, typeof(GameObject), false);

        GUI.enabled = !EditorApplication.isCompiling;

        if (GUILayout.Button("Create"))
        {
            if (Type.GetType(_popupClassName) != null)
            {
                Debug.LogError(_popupClassName + " was exist");
                return;
            }

            var rootPath = Directory.GetParent(Application.dataPath);
            var scriptPath = _path + "/" + _popupClassName + "/Scripts";
            var resourcePath = _path + "/" + _popupClassName + "/Resources/Popups";

            Directory.CreateDirectory(rootPath + "/" + scriptPath);
            Directory.CreateDirectory(rootPath + "/" + resourcePath);
            AssetDatabase.Refresh();
            var obj = ClonePrefabs(_popupClassName, resourcePath);

            CreatePopupClassAsync(obj, scriptPath);
        }

        if (GUILayout.Button("Add Component"))
        {
            var resourcePath = _path + "/" + _popupClassName + "/Resources/Popups";
            AddComponent(resourcePath, _popupClassName);
        }
    }

    private GameObject ClonePrefabs(string name, string toPath)
    {
        var filePath = toPath + "/" + name + ".prefab";
        AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(_tempPopup), filePath);
        AssetDatabase.Refresh();
        var popup = (GameObject)AssetDatabase.LoadAssetAtPath(filePath, typeof(GameObject));
        return popup;
    }

    private void AddComponent(string objectPath, string name)
    {
        GameObject popup = (GameObject)AssetDatabase.LoadAssetAtPath(objectPath + "/" + name + ".prefab", typeof(GameObject));

        if (popup == null)
        {
            Debug.LogError("The popup not found: " + name);
            return;
        }

        System.Type MyScriptType = System.Type.GetType(name + ", Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");

        if (popup == null)
        {
            Debug.LogError("The class not found: " + name);
            return;
        }
        popup.AddComponent(MyScriptType);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        AssetDatabase.OpenAsset(popup);
    }

    private void CreatePopupClassAsync(GameObject obj, string fullPath)
    {
        string copyPath = fullPath + "/" + obj.name + ".cs";

        if (File.Exists(copyPath) == false)
        { // do not overwrite
            using (StreamWriter outfile = new StreamWriter(copyPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using System.Collections;");
                outfile.WriteLine("public class " + obj.name + " : BasePopup");
                outfile.WriteLine("{");
                outfile.WriteLine("    public static string NAME_POPUP = \"" + obj.name + "\";");
                outfile.WriteLine("    public static " + obj.name + " ShowPopup()");
                outfile.WriteLine("    {");
                outfile.WriteLine("        Hashtable hashtable = new Hashtable();");
                outfile.WriteLine("        return PopupController.Instance.ShowPopup(NAME_POPUP, hashtable) as " + obj.name + ";");
                outfile.WriteLine("    }");
                outfile.WriteLine("    ");
                outfile.WriteLine("    public static void HidePopup()");
                outfile.WriteLine("    {");
                outfile.WriteLine("        PopupController.Instance.HidePopup(NAME_POPUP);");
                outfile.WriteLine("    }");
                outfile.WriteLine("    ");
                outfile.WriteLine("    //CODE_HERE");
                outfile.WriteLine("    ");
                outfile.WriteLine("    protected override void OnShow(object data)");
                outfile.WriteLine("    {");
                outfile.WriteLine("        base.OnShow(data);");
                outfile.WriteLine("        if (data != null)");
                outfile.WriteLine("        {");
                outfile.WriteLine("             Hashtable hashtable = data as Hashtable;");
                outfile.WriteLine("        }");
                outfile.WriteLine("    }");
                outfile.WriteLine("    ");
                outfile.WriteLine("    protected override void OnHide()");
                outfile.WriteLine("    {");
                outfile.WriteLine("        base.OnHide();");
                outfile.WriteLine("    }");
                outfile.WriteLine("}");
            }//File written
        }
        AssetDatabase.Refresh();
        Debug.Log("Creating Classfile: " + copyPath);
    }
}