using System;
using UnityEngine;

public class ItemUp : Upgrader
{
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private int _durabilityUp;
    [SerializeField] private float _moneyIncrease;
    [SerializeField] private GameRestarter _gameRestarter;

    public int DurabilityUp => _durabilityUp;
    public float MoneyIncrease => _moneyIncrease;

    private void OnEnable()
    {
        _spawner.PartStatsSetted += OnPartStatsSetted;
    }

    private void OnDisable()
    {
        _spawner.PartStatsSetted -= OnPartStatsSetted;
    }

    private void OnPartStatsSetted()
    {
        CalculateStats(_spawner.StartDurability, _spawner.PartStats.MaxDurability, _durabilityUp);
    }

    protected override void UpgradeTarget()
    {
        _spawner.UpgradeParts(_durabilityUp, _moneyIncrease);
    }
}
