using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private ItemUp _itemUp;
    [SerializeField] private List<Item> _items;
    [SerializeField] private int _levelItemUp;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PressEnergy _pressEnergy;
    [SerializeField] private GameRestarter _restarter;
    [SerializeField] private PartStats _partStats;

    private Item _currentItem;
    private bool _isCurrentItemBeDestroyed;

    public event Action<Item> Spawned;

    public IReadOnlyCollection<Item> Items => _items;
    public Item CurrentItem => _currentItem;
    public bool IsCurrentItemBeDestroyed => _isCurrentItemBeDestroyed;

    private void Start()
    {
        Execute();
    }

    public void SetItems(IReadOnlyCollection<Item> items)
    {
        _items = items.ToList();
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

        Spawned?.Invoke(_currentItem);

        if (_isCurrentItemBeDestroyed)
            return;

        _currentItem.Destroyed += OnDestroyed;
    }

    private Item GetItem()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException(nameof(GetItem));

        int itemNumber = Convert.ToInt32(_itemUp.Level / _levelItemUp);

        if (itemNumber >= _items.Count)
            AddSimularItems();

        return _items[itemNumber];
    }

    private void AddSimularItems()
    {
        _items.AddRange(_items);
    }

    private void OnDestroyed(Item item)
    {
        _isCurrentItemBeDestroyed = true;
        _currentItem.Destroyed -= OnDestroyed;
    }
}
