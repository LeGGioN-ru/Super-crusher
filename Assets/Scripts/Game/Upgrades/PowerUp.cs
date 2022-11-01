using UnityEngine;

public class PowerUp : Upgrader
{
    [SerializeField] private Press _press;
    [SerializeField] private int _addPower;

    protected override void UpgradeTarget()
    {
        _press.UpgradePower(_addPower);
    }
}
