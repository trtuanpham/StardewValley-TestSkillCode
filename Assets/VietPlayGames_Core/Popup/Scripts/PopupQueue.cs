using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupQueue : MonoBehaviour
{
    public static PopupQueue Instance;

    private const string POPUP_KEY = "popup";
    private const string DATA_KEY = "data";
    private List<Hashtable> _popupQueues = new List<Hashtable>();

    private object _lock =new object();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        StartCoroutine(T_UpdateQueue());
    }

    private IEnumerator T_UpdateQueue()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (!PopupController.Instance.HasActivePopup)
            {
                OnProcessShowPopup();
            }
            yield return new WaitForSeconds(1);
        }
    }

    protected virtual void OnProcessShowPopup()
    {
        lock (_lock)
        {
            if (_popupQueues.Count > 0)
            {
                var popupHasTable = _popupQueues[0];
                _popupQueues.RemoveAt(0);
                var popupName = (string)popupHasTable[POPUP_KEY];
                var popupData = (object)popupHasTable[DATA_KEY];
                PopupController.Instance.ShowPopup(popupName, popupData);
            }
        }
    }

    public void AddPopup(string popup, object data = null)
    {
        Hashtable hashtable = new Hashtable();
        hashtable.Add(POPUP_KEY, popup);
        hashtable.Add(DATA_KEY, data);
        _popupQueues.Add(hashtable);
    }

    public void RemoveShowPopup(string popup)
    {
        lock (_lock)
        {
            for (int i = _popupQueues.Count - 1; i >= 0; i--)
            {
                if (((string)_popupQueues[i][POPUP_KEY]) == popup)
                {
                    _popupQueues.RemoveAt(i);
                }
            }
        }
    }

    public void RemoveAll()
    {
        _popupQueues.Clear();
    }
}
