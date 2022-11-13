using System;
using UnityEngine;

public class EnergyUp : Upgrader
{
    [SerializeField] private PressEnergy _pressEnergy;
    [SerializeField] private int _addEnergy;

    private void OnEnable()
    {
        _pressEnergy.EnergySetted += OnEnergySetted;
    }

    private void OnDisable()
    {
        _pressEnergy.EnergySetted -= OnEnergySetted;
    }

    private void OnEnergySetted()
    {
        CalculateStats(_pressEnergy.StartEnergy, _pressEnergy.Energy, _addEnergy);
    }

    protected override void UpgradeTarget()
    {
        _pressEnergy.UpgradeEnergy(_addEnergy);
    }
}
