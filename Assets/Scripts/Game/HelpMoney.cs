using Agava.YandexGames;
using System;
using UnityEngine;

public class HelpMoney : MonoBehaviour
{
    [SerializeField] private PowerUp _powerUp;
    [SerializeField] private int _amountUpgrades;
    [SerializeField] private Wallet _wallet;

    private VideoAdvertisingShower _videoAdvertisingShower;

    public int HelpMoneyAdd => Convert.ToInt32(_powerUp.Price * _powerUp.PriceIncrease * _amountUpgrades);

    private void Start()
    {
        _videoAdvertisingShower = new VideoAdvertisingShower();
    }

    public void ShowAd()
    {
        _videoAdvertisingShower.Execute(Execute);
    }

    private void Execute()
    {
        _wallet.AddMoney(HelpMoneyAdd);
    }
}
