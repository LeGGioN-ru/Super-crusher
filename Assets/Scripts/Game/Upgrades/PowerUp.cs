using UnityEngine;

public class PowerUp : Upgrader
{
    [SerializeField] private Press _press;
    [SerializeField] private int _addPower;

    private void OnEnable()
    {
        _press.PowerSetted += OnPowerSetted;
    }

    private void OnDisable()
    {
        _press.PowerSetted -= OnPowerSetted;
    }

    private void OnPowerSetted()
    {
        CalculateStats(_press.StartPower, _press.CurrentPower, _addPower);
    }

    protected override void UpgradeTarget()
    {
        _press.UpgradePower(_addPower);
    }
}
