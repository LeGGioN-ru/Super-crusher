using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _currentMoney;

    public int CurrentMoney => _currentMoney;

    public event Action<int> MoneyAdded;
    public event Action<int> MoneyReduced;

    public void AddMoney(int money)
    {
        _currentMoney += money;
        MoneyAdded?.Invoke(money);
    }

    public void ReduceMoney(int money)
    {
        if (_currentMoney < money)
            throw new InvalidOperationException(nameof(ReduceMoney));

        _currentMoney -= money;
        MoneyReduced?.Invoke(money);
    }
}
