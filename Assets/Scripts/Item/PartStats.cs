using System;
using UnityEngine;

[Serializable]
public class PartStats : IReadablePartStats
{
    [SerializeField] private long _money;
    [SerializeField] private int _durability;

    private readonly int _maxDurability;

    public int MaxDurability => _maxDurability;
    public long Money => _money;
    public int Durability => _durability;

    public PartStats(long money, int durability)
    {
        _money = money;
        _durability = durability;
        _maxDurability = durability;
    }

    public PartStats(PartStats partStats)
    {
        _money = partStats.Money;
        _durability = partStats.Durability;
        _maxDurability = partStats.Durability;
    }

    public void Merge(int durability, float moneyIncrease)
    {
        _money = Convert.ToInt64(_money * moneyIncrease);
        _durability += durability;
    }

    public void ReduceDurability(int damage)
    {
        _durability -= damage;
    }
}
