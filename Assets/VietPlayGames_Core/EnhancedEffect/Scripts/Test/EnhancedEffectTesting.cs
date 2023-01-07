using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedEffectTesting : MonoBehaviour
{
    [SerializeField] GameObject _target;

    public void ReactiveGameObject()
    {
        StartCoroutine(IE_ReactiveGameObject());
    }

    private IEnumerator IE_ReactiveGameObject()
    {
        _target.SetActive(false);
        yield return null;
        _target.SetActive(true);
    }
}
