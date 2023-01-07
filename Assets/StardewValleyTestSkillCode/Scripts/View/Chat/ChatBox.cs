using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _contentText;

    public ChatBox SetData(string content)
    {
        _contentText.text = content;
        return this;
    }
}
