using UnityEngine;

public class ItemUp : Upgrader
{
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private PartStats _partStatsUpgrade;
    [SerializeField] private GameRestarter _gameRestarter;

    protected override void UpgradeTarget()
    {
        _spawner.UpgradeParts(_partStatsUpgrade);
    }
}
