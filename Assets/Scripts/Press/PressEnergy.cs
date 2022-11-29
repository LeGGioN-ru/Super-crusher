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
    private int _startEnergy;

    public int Energy => _energy;
    public int StartEnergy => _startEnergy;

    public event Action<float> Changed;
    public event Action EnergyEnded;
    public event Action<float> EnergySetted;

    private void Awake()
    {
        _press = GetComponent<Press>();
        _startEnergy = _energy;
    }

    private void Start()
    {
        ResetEnergy();
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

    public void SetEnergy(int energy)
    {
        if (_energy <= 0)
            throw new ArgumentException(nameof(Energy));

        _energy = energy;
        ResetEnergy();
        EnergySetted?.Invoke(_energy);
    }

    public void UpgradeEnergy(int powerUp)
    {
        _energy += powerUp;
        ResetEnergy();
    }

    private void ResetEnergy()
    {
        _energyReduced = _press.CurrentPower * _percentEnergyReducing;
        _currentEnergy = _energy;

        Changed?.Invoke(_currentEnergy);
    }

    private void OnPartHitted()
    {
        _currentEnergy -= _energyReduced;

        if (_currentEnergy <= 0)
            DisableEnergy();

        Changed?.Invoke(_currentEnergy);
    }

    private void DisableEnergy()
    {
        EnergyEnded?.Invoke();
        _restarter.Execute();
    }
}
