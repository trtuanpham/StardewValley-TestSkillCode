using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeObject : MonoBehaviour
{
    [SerializeField] CoinObject _baseCoinObject;
    [SerializeField] Transform _root;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == CharacterControl.TAG)
        {
            _root.DOShakeRotation(0.5f, 10, 10);

            if (Random.Range(0f, 1f) < 0.3f)
            {
                int coin = Random.Range(1, 5);
                for (int i = 0; i < coin; i++)
                {
                    var coinObject = _baseCoinObject.Clone();
                    coinObject.transform.position = transform.position;
                    coinObject.transform.DOMove(transform.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f)), 1f);
                }
            }
        }
    }
}
