using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerObserverManager
{
    public static Action<int> OnPlayerCoinsChanged;

    public static void PlayerCoinsChanged(int value)
    {
        OnPlayerCoinsChanged?.Invoke(value);
    }
    
    public static Action<int> OnPlayerColetaveisChanged;

    public static void PlayerColetaveisChanged(int value)
    {
        OnPlayerColetaveisChanged?.Invoke(value);
    }
    




}
