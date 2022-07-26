using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coletaveis : MonoBehaviour
{
    [SerializeField] private TMP_Text _coletaveisText;
    private void OnEnable()
    {
        PlayerObserverManager.OnPlayerColetaveisChanged += UpdateColetaveisText;

    }

    private void OnDisable()
    {
        PlayerObserverManager.OnPlayerColetaveisChanged -= UpdateColetaveisText;
    }

    private void UpdateColetaveisText(int coletaveis)
    {
        _coletaveisText.text = coletaveis.ToString();
    }
}
