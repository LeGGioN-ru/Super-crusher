using System;
using UnityEngine;

public abstract class Upgrader : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private SoundSettings _soundSettings;
    [SerializeField] protected long Price;
    [SerializeField] private float _priceIncrease;

    protected int Level;

    public event Action<int, long> Changed;

    public Wallet Wallet => _wallet;
    public int CurrentLevel => Level;
    public long CurrentPrice => Price;
    public float PriceIncrease => _priceIncrease;

    public void Execute()
    {
        if (_wallet.CurrentMoney < Price)
            return;

        _soundSettings.PlaySound();
        TryPlayParticles();

        _wallet.ReduceMoney(Price);
        UpgradeTarget();
        Price = Convert.ToInt64(Price * _priceIncrease);
        Level++;
        Changed?.Invoke(Level, Price);
    }

    abstract protected void UpgradeTarget();

    protected void CalculateStats(int startValue, int maxValue, int addValue)
    {
        Level = CalculateLevel(startValue, maxValue, addValue);

        for (int i = 0; i < Level; i++)
            Price = Convert.ToInt64(Price * PriceIncrease);

        Changed?.Invoke(Level, Price);
    }

    private int CalculateLevel(int startValue, int maxValue, int addValue)
    {
        return (maxValue - startValue) / addValue;
    }

    private void TryPlayParticles()
    {
        if (_particleSystem.isPlaying == false)
            _particleSystem.Play();
    }
}