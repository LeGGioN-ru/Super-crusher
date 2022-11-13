using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private long _currentMoney;

    public long CurrentMoney => _currentMoney;

    public event Action<long> MoneyAdded;
    public event Action<long> MoneyReduced;

    public void SetCurrentMoney(long money)
    {
        _currentMoney = money;
        MoneyAdded?.Invoke(_currentMoney);
    }

    public void AddMoney(long money)
    {
        _currentMoney += money;
        MoneyAdded?.Invoke(money);
    }

    public void ReduceMoney(long money)
    {
        if (_currentMoney < money)
            throw new InvalidOperationException(nameof(ReduceMoney));

        _currentMoney -= money;
        MoneyReduced?.Invoke(money);
    }
}
