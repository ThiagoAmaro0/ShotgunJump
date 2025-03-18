using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinSO", menuName = "CoinSO", order = 0)]
public class CoinSO : ScriptableObject
{
    private int _currentValue;
    private int _maxValue;
    public Action<int> onCollect;
    public Action onMax;

    public int MaxValue { get => _maxValue; }
    public int CurrentValue { get => _currentValue; }

    public void Setup(int maxCoins)
    {
        _currentValue = 0;
        _maxValue = maxCoins;
        onCollect.Invoke(_currentValue);
    }

    public void Collect(int value = 1)
    {
        _currentValue += value;
        if (_currentValue >= _maxValue)
        {
            _currentValue = _maxValue;
            onMax?.Invoke();
        }
        onCollect.Invoke(_currentValue);
    }
}