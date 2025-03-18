using System;
using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    [SerializeField] private CoinSO _coin;
    [SerializeField] private TMP_Text _text;

    void OnEnable()
    {
        _coin.onCollect += UpdateText;
        UpdateText(_coin.CurrentValue);
    }

    void OnDisable()
    {
        _coin.onCollect -= UpdateText;
    }

    private void UpdateText(int value)
    {
        _text.text = $"{value}/{_coin.MaxValue}";
    }
}