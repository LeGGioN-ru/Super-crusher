using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private float _moneyAddPercent;

    private int _currentMoney;

    public int CurrentMoney => _currentMoney;

    public event Action<int> MoneyChanged;

    public void AddMoney(int money)
    {
        _currentMoney += Convert.ToInt32(money * _moneyAddPercent);
        MoneyChanged?.Invoke(_currentMoney);
    }

    public void ReduceMoney(int money)
    {
        if (_currentMoney < money)
            throw new InvalidOperationException(nameof(ReduceMoney));

        _currentMoney -= money;
        MoneyChanged?.Invoke(_currentMoney);
    }
}
