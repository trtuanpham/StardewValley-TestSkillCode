using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class ControlSoundButton : BaseMonoBehaviour
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private GameObject _disableObject;
    [SerializeField] GameObject _enableObject;

    protected override void OnInit()
    {
        base.OnInit();

        if (_disableObject != null)
        {
            _disableObject.SetActive(Sound2D.SoundVolume == 0);
        }

        if (_enableObject != null)
        {
            _enableObject.SetActive(Sound2D.SoundVolume != 0);
        }
    }

    protected override void OnAddEvent()
    {
        base.OnAddEvent();
        _button.onClick.AddListener(OnClickButtonHandler);// += _button_UpdateActiveHandler;
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();
        _button.onClick.RemoveListener(OnClickButtonHandler);
    }

    private void OnClickButtonHandler()
    {
        Sound2D.SoundVolume = (Sound2D.SoundVolume == 0) ? Sound2D.DEFAULT_SOUND_VOLUME : 0;

        if (_disableObject != null)
        {
            _disableObject.SetActive(Sound2D.SoundVolume == 0);
        }

        if (_enableObject != null)
        {
            _enableObject.SetActive(Sound2D.SoundVolume != 0);
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (_button == null && !Application.isPlaying)
        {
            _button = gameObject.GetComponent<Button>();
        }
    }
#endif
}
