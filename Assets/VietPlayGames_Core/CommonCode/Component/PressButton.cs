using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool interactable = true;
    public UnityEvent OnClickDownHandler;
    public UnityEvent OnClickUpHandler;

    [SerializeField] Image _image;
    [SerializeField] Color _normalColor;
    [SerializeField] Color _pressColor;

    private void Start()
    {
        _image.color = _normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable)
        {
            return;
        }
        OnClickDownHandler?.Invoke();
        _image.color = _pressColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!interactable)
        {
            return;
        }
        OnClickUpHandler?.Invoke();
        _image.color = _normalColor;
    }

    private void OnValidate()
    {
        if (_image == null)
        {
            _image = GetComponent<Image>();
        }
    }
}
