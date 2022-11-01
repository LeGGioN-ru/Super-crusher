using System;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PressEnergy _pressEnergy;
    [SerializeField] private GameRestarter _restarter;
    [SerializeField] private PartStats _partStats;

    private Item _currentItem;
    private bool _isCurrentItemBeDestroyed;

    public bool IsCurrentItemBeDestroyed => _isCurrentItemBeDestroyed;

    private void Start()
    {
        Execute();
    }

    public void UpgradeParts(PartStats partStats)
    {
        _partStats.Merge(partStats);
        _isCurrentItemBeDestroyed = false;
    }

    public void Execute()
    {
        _currentItem = Instantiate(_item, _spawnPoint);
        _currentItem.Init(_pressEnergy, _restarter, _partStats);

        if (_isCurrentItemBeDestroyed)
            return;

        _currentItem.Destroyed += OnDestroyed;
    }

    private void OnDestroyed()
    {
        _isCurrentItemBeDestroyed = true;
        _currentItem.Destroyed -= OnDestroyed;
    }
}
