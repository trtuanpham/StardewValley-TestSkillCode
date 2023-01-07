using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRandomObject2D : MonoBehaviour
{
    [SerializeField] float _fromValue = -10;
    [SerializeField] float _toValue = 10;
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(_fromValue, _toValue));
    }
}
