using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinPlayerBox : BaseMonoBehaviour
{
    [SerializeField] ValueBox _coinBox;

    protected override void OnInit()
    {
        base.OnInit();
        _coinBox.SetData(PlayerController.Instance.coin);
    }

    protected override void OnAddEvent()
    {
        base.OnAddEvent();
        PlayerController.Instance.OnUpdatedCoinHandler += PlayerController_OnUpdatedCoinHandler;
    }

    protected override void OnRemoveEvent()
    {
        base.OnRemoveEvent();
        PlayerController.Instance.OnUpdatedCoinHandler += PlayerController_OnUpdatedCoinHandler;
    }

    private void PlayerController_OnUpdatedCoinHandler(long coin, long coinChange)
    {
        _coinBox.SetData(coin);
    }
}
