using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Animator _animator;
    [SerializeField] float _runSpeed = 20.0f;

    private float _horizontal;
    private float _vertical;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_horizontal * _runSpeed * Time.smoothDeltaTime,0, _vertical * _runSpeed * Time.smoothDeltaTime);

        if (_rigidbody.velocity.magnitude > 0)
        {
            _animator.SetBool("isRun", true);
        }
        else
        {
            _animator.SetBool("isRun", false);
        }
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}
