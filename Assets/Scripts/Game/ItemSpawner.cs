using System;
using System.Collections.Generic;
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
    private int _startDurability;
    private int _amountRepeats;

    public event Action<Item> Spawned;
    public event Action PartStatsSetted;

    public int AmountRepeats => _amountRepeats;
    public int StartDurability => _startDurability;
    public IReadablePartStats PartStats => _partStats;
    public bool IsCurrentItemBeDestroyed => _isCurrentItemBeDestroyed;

    private void Start()
    {
        Execute();
        _startDurability = _partStats.MaxDurability;
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

    private void OnDestroyed(Item item)
    {
        _isCurrentItemBeDestroyed = true;
        _currentItem.Destroyed -= OnDestroyed;
    }

    public void SetSave(PartStats partStats, int amountItemsRepeat)
    {
        _partStats = partStats;
        PartStatsSetted?.Invoke();

        for (int i = 0; i < amountItemsRepeat; i++)
            _items.AddRange(_items);

        Destroy(_currentItem.gameObject);
        Execute();
    }

    public void UpgradeParts(int durabilityIncrease, float priceIncrease)
    {
        _isCurrentItemBeDestroyed = false;
        _partStats.Merge(durabilityIncrease, priceIncrease);
    }

    private void AddSimularItems()
    {
        _items.AddRange(_items);
        _amountRepeats++;
    }
}
