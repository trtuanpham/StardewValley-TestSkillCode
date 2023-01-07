using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockButtonOneClick : BaseButtonControl
{
    protected override void OnClickButtonHandler()
    {
        base.OnClickButtonHandler();
        _button.interactable = false;

        StartCoroutine(IE_AutoEnable());
    }

    private IEnumerator IE_AutoEnable()
    {
        yield return new WaitForSeconds(1);
        _button.interactable = true;
    }
}
