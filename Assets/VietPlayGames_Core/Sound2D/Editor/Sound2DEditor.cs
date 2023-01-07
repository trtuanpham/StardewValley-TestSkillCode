using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Text;

[CustomEditor(typeof(Sound2DEditor))]
public class Sound2DEditor : Editor
{
    [MenuItem("Assets/Sound2D/Create AudioData")]
    static void CreateMyAsset()
    {
        var obj = Selection.activeObject;
        string path = AssetDatabase.GetAssetPath(obj);
        string folderPath = Path.GetDirectoryName(path);

        if(obj.GetType()!= typeof(AudioClip))
        {
            throw new System.Exception(obj.GetType() + " not support");
        }

        string assetPath = Path.GetDirectoryName(path);
        string assetId = ToSnakeCase(obj.name);

        assetPath = folderPath + "/" + assetId + ".asset";

        Sound2DAudioData asset = CreateInstance<Sound2DAudioData>();
        AssetDatabase.CreateAsset(asset, assetPath);
        AssetDatabase.SaveAssets();

        asset.id = assetId;
        asset.audioClip = (AudioClip)obj;
        asset.autioType = Sound2DAudioData.AudioType.SFX;
        asset.volume = 1;

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
        UnityEngine.Debug.Log("generated AudioData!!!: "+ assetPath);
    }

    static string ToSnakeCase(string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }
        if (text.Length < 2)
        {
            return text;
        }
        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));
        for (int i = 1; i < text.Length; ++i)
        {
            char c = text[i];
            if (char.IsUpper(c))
            {
                sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}
