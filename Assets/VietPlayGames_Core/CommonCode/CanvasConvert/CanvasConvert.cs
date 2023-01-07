using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(CanvasScaler))]
[ExecuteInEditMode]
public class CanvasConvert : MonoBehaviour
{
    public CanvasConvertSetting _canvasConvertSetting;
    public CanvasScaler _canvasScaler;
    public bool _useOwnerSetting = false;
    public Vector2 _ownerSizeTablet = new Vector2(1000, 1000);
    public Vector2 _ownerSizeMobile = new Vector2(750, 750);

    private Vector2 _sizeTablet = new Vector2(1000, 1000);
    private Vector2 _sizeMobile = new Vector2(750, 750);

    private float _width;
    private float _height;

    // Use this for initialization
    void Awake()
    {
        if (_canvasConvertSetting == null)
        {
            _canvasConvertSetting = CanvasConvertSetting.Instance;
        }

        InitSetting();

        UpdateResolution(_canvasScaler);
    }

    private void InitSetting()
    {
        if (!_useOwnerSetting)
        {
            _sizeTablet = _canvasConvertSetting.SizeTablet;
            _sizeMobile = _canvasConvertSetting.SizeMobile;
        }
        else
        {
            _sizeTablet = _ownerSizeTablet;
            _sizeMobile = _ownerSizeMobile;
        }
    }

    // Update is called once per frame
#if UNITY_EDITOR
    void Update()
    {
        if (Application.isPlaying)
        {
            return;
        }

        if (_height == Screen.height && _width == Screen.width)
        {
            return;
        }

        InitSetting();

        if (_canvasScaler != null)
        {
            UpdateResolution(_canvasScaler);
            _height = Screen.height;
            _width = Screen.width;
        }
    }
#endif

    private void UpdateResolution(CanvasScaler canvasScaler)
    {
        if (canvasScaler != null)
        {
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            if (DeviceDetect.IsTabletResolution())
            {
                canvasScaler.referenceResolution = _sizeTablet;
            }
            else
            {
                canvasScaler.referenceResolution = _sizeMobile;
            }
#elif UNITY_EDITOR
            if (DeviceDiagonalSizeInInches() > 6.5f)
            {
                canvasScaler.referenceResolution = _sizeTablet;
            }
            else
            {
                canvasScaler.referenceResolution = _sizeMobile;
            }
#endif
        }
    }

#if UNITY_EDITOR
    private float DeviceDiagonalSizeInInches()
    {
        float dpi = Screen.dpi <= 0 ? _canvasConvertSetting.DPI : Screen.dpi;

        if (Application.isEditor)
        {
            dpi = _canvasConvertSetting.EDITOR_DPI;
        }

        var size = UnityEditor.Handles.GetMainGameViewSize();

        float screenWidth = size.x / dpi;
        float screenHeight = size.y / dpi;
        float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

        Debug.Log(dpi + "Getting device inches: " + diagonalInches);

        return diagonalInches;
    }
#endif

    private void OnValidate()
    {
        if (_canvasScaler == null)
        {
            _canvasScaler = GetComponent<CanvasScaler>();
        }

        if (!_useOwnerSetting)
        {
            if (_canvasConvertSetting == null)
            {
                _canvasConvertSetting = Resources.Load(CanvasConvertSetting.SETTING_PATH) as CanvasConvertSetting;
            }
        }
    }
}
