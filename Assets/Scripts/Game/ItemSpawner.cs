using System;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private ItemUp _itemUp;
    [SerializeField] private Item[] _items;
    [SerializeField] private int _levelItemUp;
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
        _currentItem = Instantiate(GetItem(), _spawnPoint);
        _currentItem.Init(_pressEnergy, _restarter, _partStats);

        if (_isCurrentItemBeDestroyed)
            return;

        _currentItem.Destroyed += OnDestroyed;
    }

    private Item GetItem()
    {
        if (_items.Length == 0)
            throw new InvalidOperationException(nameof(GetItem));

        if (_itemUp.Level == 0)
            return _items[0];

        return _items[Convert.ToInt32(_itemUp.Level / _levelItemUp)];
    }

    private void OnDestroyed()
    {
        _isCurrentItemBeDestroyed = true;
        _currentItem.Destroyed -= OnDestroyed;
    }
}
