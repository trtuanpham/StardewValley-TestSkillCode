using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SameButton : BaseButtonControl
{
    [SerializeField] Button _targetButton;

    protected override void OnClickButtonHandler()
    {
        base.OnClickButtonHandler();
        if (_targetButton.gameObject.activeSelf)
        {
            ExecuteEvents.Execute<IPointerClickHandler>(_targetButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        }
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        if (_targetButton != null)
        {
            gameObject.name = "[SameButton]" + _targetButton.name;
        }
        else
        {
            gameObject.name = "[SameButton]#None";
        }
    }
}
