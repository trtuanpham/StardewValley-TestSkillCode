using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound2DAudioData : ScriptableObject
{
    public string id;
    public AudioClip audioClip;
    public AudioType autioType;
    [Range(0f, 1f)]
    public float volume;

    //public Sound2DAudioData(string id, AudioClip audioClip, AudioType type, float volume)
    //{
    //    this.id = id;
    //    this.audioClip = audioClip;
    //    autioType = type;
    //    this.volume = volume;
    //}

    public enum AudioType
    {
        SFX, Music, NPC
    }
}
