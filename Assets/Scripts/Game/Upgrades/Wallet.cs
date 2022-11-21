using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private long _currentMoney;

    public long CurrentMoney => _currentMoney;

    public event Action<long> MoneyChanged;

    public void SetCurrentMoney(long money)
    {
        _currentMoney = money;
        MoneyChanged?.Invoke(_currentMoney);
    }

    public void AddMoney(long money)
    {
        _currentMoney += money;
        MoneyChanged?.Invoke(money);
    }

    public void ReduceMoney(long money)
    {
        if (_currentMoney < money)
            throw new InvalidOperationException(nameof(ReduceMoney));

        _currentMoney -= money;
        MoneyChanged?.Invoke(money);
    }
}
