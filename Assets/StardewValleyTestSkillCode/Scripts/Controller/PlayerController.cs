using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public static PlayerController Instance = new PlayerController();

    public long coin { get; private set; }

    public event Action<long, long> OnUpdatedCoinHandler;

    public PlayerController()
    {
        coin = 300;
    }

    public void UpdateCoin(long coinChange)
    {
        coin += coinChange;
        OnUpdatedCoinHandler?.Invoke(coin, coinChange);
    }
}
