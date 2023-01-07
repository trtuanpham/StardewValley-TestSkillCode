using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class Sound2D : MonoBehaviour
{
    #region Static
    private const string KEY_SAVE_SOUND_VOLUME = "Sound_Volume";
    private const string KEY_SAVE_MUSIC_VOLUME = "music_Volume";
    private const string KEY_SAVE_NPC_VOLUME = "NPC_Volume";

    private const float TIME_EFFECT = 0.5f;

    private static Sound2D _instance;
    public static bool Initialized { get; private set; }

    public static void Init()
    {
        if (_instance == null)
        {
            _instance = new GameObject("Sound2D").AddComponent<Sound2D>();
            //_instance.gameObject.AddComponent<AudioListener>();
            _instance._sound2DSetting = Resources.Load<Sound2DSetting>("Sound/Sound2DSetting");
            if (_instance._sound2DSetting != null)
            {
                foreach (var s in _instance._sound2DSetting.SoundEffects)
                {
                    AddSound(s.id, s.audioClip, Sound2DAudioData.AudioType.SFX, s.volume);
                }

                foreach (var s in _instance._sound2DSetting.Musics)
                {
                    AddSound(s.id, s.audioClip, Sound2DAudioData.AudioType.Music, s.volume);
                }

                foreach (var s in _instance._sound2DSetting.NPCs)
                {
                    AddSound(s.id, s.audioClip, Sound2DAudioData.AudioType.NPC, s.volume);
                }
            }
            Initialized = true;
            DontDestroyOnLoad(_instance);
        }
    }

    public static void AddSetting(Sound2DSetting sound2DSetting)
    {
        if (sound2DSetting != null)
        {
            foreach (var s in sound2DSetting.SoundEffects)
            {
                AddSound(s.id, s.audioClip, Sound2DAudioData.AudioType.SFX, s.volume);
            }

            foreach (var s in sound2DSetting.Musics)
            {
                AddSound(s.id, s.audioClip, Sound2DAudioData.AudioType.Music, s.volume);
            }

            foreach (var s in sound2DSetting.NPCs)
            {
                AddSound(s.id, s.audioClip, Sound2DAudioData.AudioType.NPC, s.volume);
            }
        }
    }

    public static Sound2DAudioItem PlayRandom(string[] ids, bool loop = false, float delay = 0)
    {
        string id = ids[UnityEngine.Random.Range(0, ids.Length)];
        return Play(id, loop, delay);
    }

    public static Sound2DAudioItem Play(string id, bool loop = false, float delay = 0)
    {
        if (_blockSound)
        {
            return null;
        }

        if (_instance != null)
        {
            if (!_instance._audios.ContainsKey(id))
            {
                Debug.Log("Sound not found: " + id);
                return null;
            }

            var data = _instance._audios[id];
            if (data != null)
            {
                if (data.autioType == Sound2DAudioData.AudioType.Music && _instance.FindAudioItem(data.id) != null)
                {
                    return null;
                }
                Sound2DAudioItem audioItem = _instance.CreateAudioItem(data, delay, loop);
                _instance._listPlaying.Add(audioItem);
                return audioItem;
            }
            return null;
        }
        else
        {
            Debug.Log("Instance is null SoundManager");
            return null;
        }
    }

    public static Sound2DAudioItem Play(AudioClip audioClip, Sound2DAudioData.AudioType audioType = Sound2DAudioData.AudioType.SFX, float volume = 1, bool loop = false, float delay = 0)
    {
        if (_blockSound)
        {
            return null;
        }

        if (_instance != null)
        {
            var asset = ScriptableObject.CreateInstance<Sound2DAudioData>();
            asset.id = audioClip.GetInstanceID().ToString();
            asset.audioClip = audioClip;
            asset.autioType = audioType;
            asset.volume = volume;

            Sound2DAudioItem audioItem = _instance.CreateAudioItem(asset, delay, loop);
            _instance._listPlaying.Add(audioItem);
            return audioItem;
        }
        else
        {
            Debug.Log("Instance is null SoundManager");
            return null;
        }
    }

    public static Sound2DAudioItem Play(Sound2DAudioData audioData, bool loop = false, float delay = 0)
    {
        if (_blockSound)
        {
            return null;
        }

        if (_instance != null)
        {
            Sound2DAudioItem audioItem = _instance.CreateAudioItem(audioData, delay, loop);
            _instance._listPlaying.Add(audioItem);
            return audioItem;
        }
        else
        {
            Debug.Log("Instance is null SoundManager");
            return null;
        }
    }

    public static void AddSound(string id, AudioClip audioClip, Sound2DAudioData.AudioType type, float volume)
    {
        if (_instance != null)
        {
            var asset = ScriptableObject.CreateInstance<Sound2DAudioData>();
            asset.id = id;
            asset.audioClip = audioClip;
            asset.autioType = type;
            asset.volume = volume;

            _instance._audios.Add(id, asset);
        }
        else
        {
            Debug.Log("Instance is null SoundManager");
        }
    }

    public static void StopAllSound()
    {
        if (_instance != null)
        {
            foreach (var p in _instance._listPlaying)
            {
                if (p.Type == Sound2DAudioData.AudioType.SFX)
                {
                    p.Dispose();
                }
            }
        }
        else
        {
            Debug.Log("Instance is null SoundManager");
        }
    }

    public static void StopAll()
    {
        if (_instance != null)
        {
            foreach (var p in _instance._listPlaying)
            {
                p.Dispose();
            }
        }
        else
        {
            Debug.Log("Instance is null SoundManager");
        }
    }

    public static void Stop(Sound2DAudioItem audioItem)
    {
        if (_instance != null)
        {
            if (audioItem != null)
            {
                audioItem.Dispose();
            }
        }
        else
        {
            Debug.Log("Instance is null SoundManager");
        }
    }


    public static void Stop(string id)
    {
        if (_instance != null)
        {
            var item = _instance.FindAudioItem(id);
            if (item != null)
            {
                item.Dispose();
                // Pull(item.AudioData);
            }
        }
        else
        {
            Debug.Log("Instance is null SoundManager");
        }
    }

    public static float NPCVolume
    {
        get
        {
            if (_instance != null)
            {
                return _instance._npcVolume;
            }
            else
            {
                Debug.Log("Instance is null SoundManager");
                return 0;
            }
        }

        set
        {
            if (_instance != null)
            {
                //value = value > DEFAULT_NPC_VOLUME ? DEFAULT_NPC_VOLUME : value;

                _instance._npcVolume = value;
                _instance.ApplyAllVolumePlayingSound();
                _instance.SaveVolumeLocal();
            }
            else
            {
                Debug.Log("Instance is null SoundManager");
            }
        }
    }

    public static float MusicVolume
    {
        get
        {
            if (_instance != null)
            {
                return _instance._musicVolume;
            }
            else
            {
                Debug.Log("Instance is null SoundManager");
                return 0;
            }
        }

        set
        {
            if (_instance != null)
            {
                //value = value > DEFAULT_MUSIC_VOLUME ? DEFAULT_MUSIC_VOLUME : value;

                _instance._musicVolume = value;
                _instance.ApplyAllVolumePlayingSound();
                _instance.SaveVolumeLocal();
            }
            else
            {
                Debug.Log("Instance is null SoundManager");
            }
        }
    }

    public static float SoundVolume
    {
        get
        {
            if (_instance != null)
            {
                return _instance._soundVolume;
            }
            else
            {
                Debug.Log("Instance is null SoundManager");
                return 0;
            }
        }

        set
        {
            if (_instance != null)
            {
                //value = value > DEFAULT_SOUND_VOLUME ? DEFAULT_SOUND_VOLUME : value;
                _instance._soundVolume = value;
                _instance.ApplyAllVolumePlayingSound();
                _instance.SaveVolumeLocal();
            }
            else
            {
                Debug.Log("Instance is null SoundManager");
            }
        }
    }

    public static void SetSoundVolumeWithoutSave(float volume)
    {
        _instance._soundVolume = volume;
        _instance.ApplyAllVolumePlayingSound();
    }

    public static void SetMusicVolumeWithoutSave(float volume)
    {
        _instance._musicVolume = volume;
        _instance.ApplyAllVolumePlayingSound();
    }

    public static void SetSoundNPCWithoutSave(float volume)
    {
        _instance._npcVolume = volume;
        _instance.ApplyAllVolumePlayingSound();
    }

    #endregion

    public static float DEFAULT_MUSIC_VOLUME = 1f;
    public static float DEFAULT_SOUND_VOLUME = 1f;
    public static float DEFAULT_NPC_VOLUME = 1f;

    private float _musicVolume = DEFAULT_MUSIC_VOLUME;
    private float _soundVolume = DEFAULT_SOUND_VOLUME;
    private float _npcVolume = DEFAULT_NPC_VOLUME;

    [SerializeField] Sound2DSetting _sound2DSetting;

    private Dictionary<string, Sound2DAudioData> _audios = new Dictionary<string, Sound2DAudioData>();
    private List<Sound2DAudioItem> _listPlaying = new List<Sound2DAudioItem>();
    private object _lock = new object();

    // Use this for initialization
    private void Awake()
    {
        LoadVolumeLocal();
    }

    private void Start()
    {
        StartCoroutine(ThreadClearStopSound());
    }

    private void LoadVolumeLocal()
    {
        _musicVolume = PlayerPrefs.GetFloat(KEY_SAVE_MUSIC_VOLUME, DEFAULT_MUSIC_VOLUME);
        _soundVolume = PlayerPrefs.GetFloat(KEY_SAVE_SOUND_VOLUME, DEFAULT_SOUND_VOLUME);
        _npcVolume = PlayerPrefs.GetFloat(KEY_SAVE_NPC_VOLUME, DEFAULT_NPC_VOLUME);
    }

    private void SaveVolumeLocal()
    {
        PlayerPrefs.SetFloat(KEY_SAVE_MUSIC_VOLUME, _musicVolume);
        PlayerPrefs.SetFloat(KEY_SAVE_SOUND_VOLUME, _soundVolume);
        PlayerPrefs.SetFloat(KEY_SAVE_NPC_VOLUME, _npcVolume);
        PlayerPrefs.Save();
    }

    private void ApplyAllVolumePlayingSound()
    {
        for (int i = 0; i < _listPlaying.Count; i++)
        {
            var playing = _listPlaying[i];
            if (playing.Type == Sound2DAudioData.AudioType.Music)
            {
                playing.SetVolume(_musicVolume);
            }
            else if (playing.Type == Sound2DAudioData.AudioType.NPC)
            {
                playing.SetVolume(_npcVolume);
            }
            else
            {
                playing.SetVolume(_soundVolume);
            }
        }
    }

    private Sound2DAudioItem FindAudioItem(string id)
    {
        for (int i = _listPlaying.Count - 1; i >= 0; i--)
        {
            var item = _listPlaying[i];
            if (item != null && item.Id == id && item.IsPlaying)
            {
                return item;
            }
        }

        return null;
    }

    private IEnumerator ThreadClearStopSound()
    {
        while (true)
        {
            lock (_lock)
            {
                for (int i = _listPlaying.Count - 1; i >= 0; i--)
                {
                    var item = _listPlaying[i];
                    if (!item.IsPlaying || item == null)
                    {
                        if (item != null)
                        {
                            item.Dispose();
                        }
                        _listPlaying.RemoveAt(i);
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private Sound2DAudioItem CreateAudioItem(Sound2DAudioData audioData, float delay, bool loop)
    {
        Sound2DAudioItem audioItem = new GameObject().AddComponent<Sound2DAudioItem>();
        audioItem.transform.SetParent(transform);
        audioItem.Initalize(audioData, delay, loop);
        return audioItem;
    }

    //fix sound
    static float temporarySoundVolume;
    static float temporaryMusicVolume;
    static float temporaryNPCVolume;
    static bool _blockSound;

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            _blockSound = true;
            temporarySoundVolume = SoundVolume;
            temporaryMusicVolume = MusicVolume;
            temporaryNPCVolume = NPCVolume;

            Sound2D.SetMusicVolumeWithoutSave(0);
            Sound2D.SetSoundVolumeWithoutSave(0);
            Sound2D.SetSoundNPCWithoutSave(0);
        }
        else
        {
            Sound2D.SetMusicVolumeWithoutSave(temporaryMusicVolume);
            Sound2D.SetSoundVolumeWithoutSave(temporarySoundVolume);
            Sound2D.SetSoundNPCWithoutSave(temporaryNPCVolume);

            StartCoroutine(IE_FixSound());
        }
    }

    private IEnumerator IE_FixSound()
    {
        yield return new WaitForSeconds(2);
        Sound2D.SetMusicVolumeWithoutSave(temporaryMusicVolume);
        Sound2D.SetSoundVolumeWithoutSave(temporarySoundVolume);
        Sound2D.SetSoundNPCWithoutSave(temporaryNPCVolume);
        _blockSound = false;
    }
}