using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAutoInit : MonoBehaviour
{
    [SerializeField] Sound2DSetting[] _settings;
    // Start is called before the first frame update
    void Start()
    {
        if (!Sound2D.Initialized)
        {
            Sound2D.Init();
            foreach (var setting in _settings)
            {
                Sound2D.AddSetting(setting);
            }
        }
    }
}
