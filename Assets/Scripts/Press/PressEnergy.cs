using System;
using UnityEngine;

[RequireComponent(typeof(Press))]
public class PressEnergy : MonoBehaviour
{
    [SerializeField] private int _energy;
    [SerializeField] private float _percentEnergyReducing;
    [SerializeField] private GameRestarter _restarter;

    private Press _press;
    private float _currentEnergy;
    private float _energyReduced;

    public Action EnergyEnded;

    private void Awake()
    {
        _press = GetComponent<Press>();
    }

    private void OnEnable()
    {
        ResetEnergy();

        _press.PartHitted += OnPartHitted;
    }

    private void OnDisable()
    {
        _press.PartHitted -= OnPartHitted;
    }

    private void OnPartHitted()
    {
        _currentEnergy -= _energyReduced;

        if (_currentEnergy <= 0)
            DisableEnergy();
    }

    public void UpgradeEnergy(int powerUp)
    {
        _energy += powerUp;
    }

    private void ResetEnergy()
    {
        _energyReduced = _press.Power * _percentEnergyReducing;
        _currentEnergy = _energy;
    }

    private void DisableEnergy()
    {
        EnergyEnded?.Invoke();
        ResetEnergy();
        _restarter.Execute();
    }
}