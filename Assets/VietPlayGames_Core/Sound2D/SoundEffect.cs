using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField] string _sound2DId;
    [SerializeField] float _delay = 0f;
    // Use this for initialization

    private void OnEnable()
    {
        StartCoroutine(IE_OnEnable());
    }

    IEnumerator IE_OnEnable()
    {
        if (string.IsNullOrEmpty(_sound2DId))
        {
            yield break;
        }

        yield return new WaitUntil(() =>
        {
            return Sound2D.Initialized;
        });
        Sound2D.Play(_sound2DId, false, _delay);
    }
}
