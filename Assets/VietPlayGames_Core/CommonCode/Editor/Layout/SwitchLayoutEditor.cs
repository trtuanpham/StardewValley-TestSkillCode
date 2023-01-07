using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[InitializeOnLoad]
public class SwitchLayoutEditor : Editor
{
    public static string RootPath
    {
        get
        {
            var g = AssetDatabase.FindAssets("t:Script SwitchLayoutEditor");
            return AssetDatabase.GUIDToAssetPath(g[0]);
        }
    }

    private static bool _clickedLandscape;
    private static bool _clickedPortrait;

    private static string _folderPath;

    static SwitchLayoutEditor()
    {
        _folderPath = GetParent(RootPath);
        SceneView.duringSceneGui -= OnSceneGUI;
        SceneView.duringSceneGui += OnSceneGUI;

        UnityEditor.EditorApplication.update -= UpdateEditor;
        UnityEditor.EditorApplication.update += UpdateEditor;
    }

    private static void UpdateEditor()
    {
        if (_clickedLandscape)
        {
            _clickedLandscape = false;
            LayoutUtility.LoadLayoutFromAsset(_folderPath + "/layout_landscape.wlt");
        }

        if (_clickedPortrait)
        {
            _clickedPortrait = false;
            LayoutUtility.LoadLayoutFromAsset(_folderPath + "/layout_portrait.wlt");
        }
    }

    private static void OnSceneGUI(SceneView sceneview)
    {
        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(50, 10, 80, 70));
        {
            if (GUILayout.Button("Landscape"))
            {
                _clickedLandscape = true;
            }

            if (GUILayout.Button("Portrait"))
            {
                _clickedPortrait = true;
            }
        }
        GUILayout.EndArea();
        Handles.EndGUI();
    }

    static string GetParent(string path)
    {
        return path.Substring(0, path.LastIndexOf("/"));
    }
}
