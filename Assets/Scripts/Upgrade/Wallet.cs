using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _currentMoney;

    public event Action<int> MoneyAdded;

    public void AddMoney(int money)
    {
        _currentMoney += money;
        MoneyAdded?.Invoke(_currentMoney);
    }
}
