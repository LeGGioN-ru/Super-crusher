using System;
using UnityEngine;

public abstract class Upgrader : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private SoundSettings _soundSettings;
    [SerializeField] private long _price;
    [SerializeField] private float _priceIncrease;

    private int _level;

    public event Action<int, long> Changed;
    public event Action Executed;

    public Wallet Wallet => _wallet;
    public int Level => _level;
    public long Price => _price;

    public void Execute()
    {
        if (_wallet.CurrentMoney < _price)
            return;

        _soundSettings.PlaySound();
        TryPlayParticles();

        _wallet.ReduceMoney(_price);
        StrengthenTarget();
        _price = Convert.ToInt64(_price * _priceIncrease);
        _level++;
        Changed?.Invoke(_level, _price);
        Executed?.Invoke();
    }

    abstract protected void StrengthenTarget();

    protected void DefineCurrentStats(int startValue, int maxValue, int addValue)
    {
        _level = DefineLevel(startValue, maxValue, addValue);
        DefinePrice(_level);

        Changed?.Invoke(_level, _price);
    }

    private int DefineLevel(int startValue, int maxValue, int addValue)
    {
        return (maxValue - startValue) / addValue;
    }

    private void DefinePrice(int level)
    {
        for (int i = 0; i < level; i++)
            _price = Convert.ToInt64(_price * _priceIncrease);
    }

    private void TryPlayParticles()
    {
        if (_particleSystem.isPlaying == false)
            _particleSystem.Play();
    }
}