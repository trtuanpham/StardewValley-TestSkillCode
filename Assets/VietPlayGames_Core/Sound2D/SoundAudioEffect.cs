using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAudioEffect : MonoBehaviour
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField] Sound2DAudioData.AudioType _audioType = Sound2DAudioData.AudioType.SFX;
    [SerializeField] float _delay = 0f;
    [Range(0f, 1f)]
    float _volume = 1f;
    // Use this for initialization

    private void OnEnable()
    {
        StartCoroutine(IE_OnEnable());
    }

    IEnumerator IE_OnEnable()
    {
        if (_audioClip == null)
        {
            yield break;
        }

        yield return new WaitUntil(() =>
        {
            return Sound2D.Initialized;
        });
        Sound2D.Play(_audioClip, _audioType, _volume, false, _delay);
    }
}