using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SVTSC_AppController : MonoBehaviour
{
    public static SVTSC_AppController Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        ShopPopup.ShowPopup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
