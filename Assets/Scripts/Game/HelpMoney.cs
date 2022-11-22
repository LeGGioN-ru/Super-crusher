using Agava.YandexGames;
using System;
using UnityEngine;

public class HelpMoney : MonoBehaviour
{
    [SerializeField] private PowerUp _powerUp;
    [SerializeField] private int _amountUpgrades;
    [SerializeField] private Wallet _wallet;

    public int HelpMoneyAdd => Convert.ToInt32(_powerUp.Price * _powerUp.PriceIncrease * _amountUpgrades);

    public void ShowAd()
    {
        VideoAd.Show(null, Execute);
    }

    private void Execute()
    {
        _wallet.AddMoney(HelpMoneyAdd);
    }
}
