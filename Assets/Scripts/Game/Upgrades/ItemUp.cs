using System;
using UnityEngine;

public class ItemUp : Upgrader
{
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private GameRestarter _gameRestarter;
    [SerializeField] private int _addDurability;
    [SerializeField] private float _addMoneyIncrease;

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
        DefineCurrentStats(_spawner.StartDurability, _spawner.PartStats.MaxDurability, _addDurability);
    }

    protected override void StrengthenTarget()
    {
        _spawner.UpgradeParts(_addDurability, _addMoneyIncrease);
    }
}
