using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonoBehaviour : MonoBehaviour
{
    private bool _initialized;
    private bool _addEvent;
    private bool _isStart;

    public bool IsStart
    {
        get
        {
            return _isStart;
        }
    }

    public void Init()
    {
        if (!_initialized)
        {
            _initialized = true;
            OnInit();
        }
    }

    protected void AddEvent()
    {
        if (!_addEvent && gameObject.activeSelf)
        {
            _addEvent = true;
            OnAddEvent();
        }
    }

    protected void RemoveEvent()
    {
        if (_addEvent)
        {
            _addEvent = false;
            OnRemoveEvent();
        }
    }

    protected virtual void Start()
    {
        if (Application.isPlaying)
        {
            _isStart = true;
            OnActive();
            Init();
            AddEvent();
            OnInitAfterEvent();
        }
    }

    protected virtual void OnEnable()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (_isStart)
        {
            OnActive();
            AddEvent();
        }
    }

    protected virtual void OnDisable()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        RemoveEvent();
    }

    protected virtual void OnDestroy()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        RemoveEvent();
    }

    protected virtual void OnInit() { }
    protected virtual void OnInitAfterEvent() { }

    /// this function will be show everytime when game object active
    protected virtual void OnActive() { }
    protected virtual void OnAddEvent() { }
    protected virtual void OnRemoveEvent() { }
}