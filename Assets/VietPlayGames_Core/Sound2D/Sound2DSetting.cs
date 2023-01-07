using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Sound2DSetting))]
public class Sound2DSettingEditor : Editor
{
    private Sound2DSetting sound2DSetting;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        sound2DSetting = target as Sound2DSetting;
        if (GUILayout.Button("Make Enum"))
        {
            //string objectPath = EditorPrefs.GetString("ObjectPath"); 
            Crenerate(sound2DSetting);
            Debug.Log("made enum sound2DId");
        }
    }

    public static void Crenerate(Sound2DSetting sound2DSetting)
    {
        var path = AssetDatabase.GetAssetPath(sound2DSetting);
        path = Path.ChangeExtension(path, "cs");

        var nameClass = Path.GetFileNameWithoutExtension(path);

        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            streamWriter.WriteLine("public static class " + nameClass);
            streamWriter.WriteLine("{");

            foreach (var audio in sound2DSetting.SoundEffects)
            {
                streamWriter.WriteLine("\t public const string " + audio.id.Trim() + " = \"" + audio.id.Trim() + "\";");
            }

            foreach (var audio in sound2DSetting.Musics)
            {
                streamWriter.WriteLine("\t public const string " + audio.id.Trim() + " = \"" + audio.id.Trim() + "\";");
            }

            foreach (var audio in sound2DSetting.NPCs)
            {
                streamWriter.WriteLine("\t public const string " + audio.id.Trim() + " = \"" + audio.id.Trim() + "\";");
            }

            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();
    }
}
#endif

public class Sound2DSetting : ScriptableObject
{
    public const string PATH = "Assets/Resources/Sound/Sound2DSetting.asset";
    [Header("Sounds:")]
    [SerializeField] List<SoundData> _soundEffects;
    [Header("Musics:")]
    [SerializeField] List<SoundData> _musics;
    [Header("NPC:")]
    [SerializeField] List<SoundData> _npcs;

    public List<SoundData> SoundEffects
    {
        get
        {
            return _soundEffects;
        }
    }

    public List<SoundData> Musics
    {
        get
        {
            return _musics;
        }
    }

    public List<SoundData> NPCs
    {
        get
        {
            return _npcs;
        }
    }

#if UNITY_EDITOR

    [MenuItem("Game Add-on/Sound2D/Sound2DSetting")]
    public static void CreateMyAsset()
    {
        FileInfo fileInfo = new FileInfo(PATH);
        string directoryFullPath = fileInfo.DirectoryName;

        if (!Directory.Exists(directoryFullPath))
        {
            Directory.CreateDirectory(directoryFullPath);
        }

        if (AssetDatabase.LoadAssetAtPath(PATH, typeof(Sound2DSetting)) == null)
        {
            Sound2DSetting asset = CreateInstance<Sound2DSetting>();
            AssetDatabase.CreateAsset(asset, PATH);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
            Debug.Log("generated sound setting");
        }
        else
        {
            var asset = AssetDatabase.LoadMainAssetAtPath(PATH);
            AssetDatabase.OpenAsset(asset);
        }
    }
#endif
}

[Serializable]
public class SoundData
{
    public string id;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}

