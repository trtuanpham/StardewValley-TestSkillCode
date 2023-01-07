using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : BaseMonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Animator _animator;
    [SerializeField] float _runSpeed = 20.0f;

    [SerializeField] CharacterAvatar _characterAvatar;

    private float _horizontal;
    private float _vertical;

    protected override void OnInit()
    {
        base.OnInit();
        _characterAvatar.SetData(AvatarController.Instance.Avatar);
    }

    protected override void OnAddEvent()
    {
        base.OnAddEvent();
        AvatarController.Instance.OnEquipAvatarHandler += AvatarController_OnEquipAvatarHandler;
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();
        AvatarController.Instance.OnEquipAvatarHandler -= AvatarController_OnEquipAvatarHandler;
    }

    private void AvatarController_OnEquipAvatarHandler(AvatarType avatarType, string avatarId)
    {
        _characterAvatar.Equip(avatarType, avatarId);
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
