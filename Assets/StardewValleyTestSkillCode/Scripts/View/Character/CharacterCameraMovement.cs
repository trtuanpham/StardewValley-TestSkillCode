using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCameraMovement : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Transform _cameraTransform;
    [SerializeField] float _moveSpeed = 20;
    [SerializeField] Vector3 _offsetCamera = new Vector3(0,8,-10);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null || _cameraTransform == null)
        {
            return;
        }

        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _target.position + _offsetCamera, _moveSpeed * Time.smoothDeltaTime);
    }
}
