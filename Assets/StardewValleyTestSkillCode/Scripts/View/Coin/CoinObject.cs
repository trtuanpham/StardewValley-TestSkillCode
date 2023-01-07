using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinObject : MonoBehaviour
{
    [SerializeField] Collider _collider;
    private void Awake()
    {
        _collider.enabled = false;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        _collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == CharacterControl.TAG)
        {
            var effect = SpawnEffectHelper.Spawn("EffectCoin");
            effect.transform.position = transform.position;
            _collider.enabled = false;
            PlayerController.Instance.UpdateCoin(1);
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        if (_collider == null)
        {
            _collider = GetComponent<Collider>();
        }
    }
}
