using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class ControlNPCButton : BaseMonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] GameObject _disableObject;
    [SerializeField] GameObject _enableObject;

    protected override void OnInit()
    {
        base.OnInit();
        // _button.SetDefault(GameSound.GetMusicVolume());
        if (_disableObject != null)
        {
            _disableObject.SetActive(Sound2D.NPCVolume == 0);
        }

        if (_enableObject != null)
        {
            _enableObject.SetActive(Sound2D.NPCVolume != 0);
        }
    }

    protected override void OnAddEvent()
    {
        base.OnAddEvent();
        _button.onClick.AddListener(OnClickButtonHandler);
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();
        _button.onClick.RemoveListener(OnClickButtonHandler);
    }

    private void OnClickButtonHandler()
    {
        Sound2D.NPCVolume = (Sound2D.NPCVolume == 0) ? Sound2D.DEFAULT_NPC_VOLUME : 0;

        if (_disableObject != null)
        {
            _disableObject.SetActive(Sound2D.NPCVolume == 0);
        }

        if (_enableObject != null)
        {
            _enableObject.SetActive(Sound2D.NPCVolume != 0);
        }
    }

    private void OnValidate()
    {
        if (_button == null)
        {
            _button = gameObject.GetComponent<Button>();
        }
    }
}
