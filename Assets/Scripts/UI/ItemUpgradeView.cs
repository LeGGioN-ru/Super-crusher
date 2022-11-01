using UnityEngine;

public class ItemUpgradeView : UpgradeView
{
    [SerializeField] private ItemSpawner _spawner;

    protected override bool IsUpgradeAvalible()
    {
        return base.IsUpgradeAvalible() && _spawner.IsCurrentItemBeDestroyed;
    }
}
