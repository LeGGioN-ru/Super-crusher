using System;
using UnityEngine;

[Serializable]
public class PartStats
{
    [SerializeField] private int _money;
    [SerializeField] private int _durability;

    public int Money => _money;
    public int Durability => _durability;

    public PartStats(int money, int durability)
    {
        _money = money;
        _durability = durability;
    }

    public PartStats(PartStats partStats)
    {
        _money = partStats.Money;
        _durability = partStats.Durability;
    }

    public void Merge(PartStats partStat)
    {
        _money += partStat.Money;
        _durability += partStat.Durability;
    }

    public void ReduceDurability(int damage)
    {
        _durability -= damage;
    }
}