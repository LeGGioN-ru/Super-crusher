using System;
using UnityEngine;

public abstract class Upgrader : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private long _price;
    [SerializeField] private float _priceIncrease;

    private int _level;

    public event Action<int, long> Changed;

    public Wallet Wallet => _wallet;
    public int Level => _level;
    public long Price => _price;

    public void Execute()
    {
        if (_wallet.CurrentMoney < _price)
            return;

        TryPlayParticles();
        _wallet.ReduceMoney(_price);
        UpgradeTarget();
        _price = Convert.ToInt64(_price * _priceIncrease);
        _level++;
        Changed?.Invoke(_level, _price);
    }

    abstract protected void UpgradeTarget();

    private void TryPlayParticles()
    {
        if (_particleSystem.isPlaying == false)
            _particleSystem.Play();
    }
}