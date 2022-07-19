using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;

    private void OnEnable()
    {
        PlayerObserverManager.OnPlayerCoinsChanged += UpdateCoinText;

    }

    private void OnDisable()
    {
        PlayerObserverManager.OnPlayerCoinsChanged -= UpdateCoinText;
    }

    private void UpdateCoinText(int coins)
    {
        _coinText.text = coins.ToString();
    }
    
}
