using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterControl : BaseMonoBehaviour
{
    public const string TAG = "Character";
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Animator _animator;
    [SerializeField] float _runSpeed = 20.0f;
    [SerializeField] GameObject _talkObject;
    [SerializeField] TextMeshPro _talkContent;
    [SerializeField] CharacterAvatar _characterAvatar;

    private float _horizontal;
    private float _vertical;
    private int _targetObjectId;
    private BaseMapObject _targetObject;

    private void Awake()
    {
        _talkObject.SetActive(false);
    }

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
        if (PopupController.Instance.HasActivePopup)
        {
            _horizontal = 0;
            _vertical = 0;
            return;
        }
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_targetObject!=null)
            {
                _targetObject.CharacterAction(this);
            }
            _animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_horizontal * _runSpeed * Time.smoothDeltaTime, 0, _vertical * _runSpeed * Time.smoothDeltaTime);

        if (_rigidbody.velocity.magnitude > 0)
        {
            _animator.SetBool("isRun", true);
        }
        else
        {
            _animator.SetBool("isRun", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _targetObject = other.gameObject.GetComponent<BaseMapObject>();
        if (_targetObject != null)
        {
            _targetObjectId = other.GetInstanceID();
            _talkContent.text = _targetObject.GetActionLabel();
            _talkObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetInstanceID() == _targetObjectId)
        {
            _targetObject = null;
            _talkObject.SetActive(false);
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
