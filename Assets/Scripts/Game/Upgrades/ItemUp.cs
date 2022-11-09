using UnityEngine;

public class ItemUp : Upgrader, ISaveble
{
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private int _durabilityUp;
    [SerializeField] private float _moneyIncrease;
    [SerializeField] private GameRestarter _gameRestarter;

    public int DurabilityUp => _durabilityUp;
    public float MoneyIncrease => _moneyIncrease;

    protected override void UpgradeTarget()
    {
        _spawner.UpgradeParts(_durabilityUp, _moneyIncrease);
    }
}

public interface ISaveble
{
    public int DurabilityUp { get; }
    public float MoneyIncrease { get; }
}
