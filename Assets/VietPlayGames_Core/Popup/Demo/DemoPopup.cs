using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoPopup : BaseMonoBehaviour
{
    public void OpenAPopup()
    {
        DemoMessagePopup.ShowPopup("This is title", "This is message", () =>
        {
            Debug.Log("Clicked ok button on Message Popup");
        });
    }

    public void OpenMultiPopup()
    {
        DemoMessagePopup.ShowPopup("This is title", "This is message", () =>
        {
            Debug.Log("Clicked ok button on Message Popup");
        });

        this.RunDelay(() =>
        {
            OpenPopup2();
        },1);
    }

    private void OpenPopup2()
    {
        DemoMessagePopup.ShowPopup("This is title 2", "This is message 2", () =>
        {
            Debug.Log("Clicked ok button on Message Popup 2");
        });
    }
}
