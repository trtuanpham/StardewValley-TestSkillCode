using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LayoutByText : MonoBehaviour
{
    [SerializeField] RectOffset _padding;
    [SerializeField] LayoutElement _layoutElement;
    [SerializeField] TextMeshProUGUI _text;
    // Start is called before the first frame update

    //// Update is called once per frame
    void Update()
    {
        UpdateLayout();
    }

    private void UpdateLayout()
    {
        if (_text != null)
        {
            _layoutElement.preferredHeight = _padding.top + _text.bounds.size.y + _padding.bottom;
        }
    }

    private void OnValidate()
    {
        if (_layoutElement == null)
        {
            _layoutElement = GetComponent<LayoutElement>();
        }
    }
}
