using UnityEngine;

public class EnergyUp : Upgrader
{
    [SerializeField] private PressEnergy _pressEnergy;
    [SerializeField] private int _addEnergy;

    protected override void UpgradeTarget()
    {
        _pressEnergy.UpgradeEnergy(_addEnergy);
    }
}
