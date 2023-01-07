using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Sound2DAudioItem : MonoBehaviour
{
    private const float TIME_EFFECT_FAST = 0.5f;
    private const float TIME_EFFECT = 1f;

    public bool IsDestroy { get; private set; }

    //public bool IsBackground { get; private set; }
    private float _delay = 0;
    private bool _isloop = false;
    private AudioSource _audioSource;
    [SerializeField] Sound2DAudioData _audioData;

    public string Id
    {
        get
        {
            return _audioData.id;
        }
    }


    public Sound2DAudioData.AudioType Type
    {
        get
        {
            return _audioData.autioType;
        }
    }

    public bool IsPlaying
    {
        get
        {
            return !IsDestroy && _audioSource != null && _audioSource.isPlaying;
        }
    }

    public void SetVolume(float volume)
    {
        var soundVolume = _audioData.volume * volume;
        _audioSource.DOKill();
        _audioSource.DOFade(soundVolume, TIME_EFFECT);
    }

    public void Initalize(Sound2DAudioData audioData, float delay, bool isloop)
    {
        _audioData = audioData;
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
        _delay = delay;
        _isloop = isloop;

        _audioSource.name = _audioData.id + "[" + _audioData.autioType + "]";
        _audioSource.clip = _audioData.audioClip;
        _audioSource.loop = _isloop;

        float soundVolume = 0;
        float time = 0;

        if (_audioData.volume <= 0)
        {
            Debug.LogWarning(_audioData.id + " have volume <= 0!!!");
        }

        if (_audioData.autioType == Sound2DAudioData.AudioType.Music)
        {
            soundVolume = _audioData.volume * Sound2D.MusicVolume;
            time = TIME_EFFECT;
        }
        else if (_audioData.autioType == Sound2DAudioData.AudioType.NPC)
        {
            soundVolume = _audioData.volume * Sound2D.NPCVolume;
            time = TIME_EFFECT_FAST;
        }
        else
        {
            soundVolume = _audioData.volume * Sound2D.SoundVolume;
            time = TIME_EFFECT_FAST;
        }

        if (_audioData.autioType == Sound2DAudioData.AudioType.SFX || _audioData.autioType == Sound2DAudioData.AudioType.NPC)
        {
            _audioSource.volume = soundVolume;
            _audioSource.PlayDelayed(_delay);
        }
        else
        {
            _audioSource.volume = 0;
            _audioSource.Play();
            _audioSource.DOFade(soundVolume, time).SetDelay(_delay);
        }
    }

    public void Dispose()
    {
        _audioSource.DOKill();
        IsDestroy = true;
        if (Type == Sound2DAudioData.AudioType.Music)
        {
            _audioSource.DOFade(0, TIME_EFFECT).OnComplete(() =>
                 {
                     _audioSource.Stop();
                     Destroy(gameObject);
                 });
        }
        else
        {
            _audioSource.Stop();
            Destroy(gameObject);
        }
    }
}