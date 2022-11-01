using System;
using UnityEngine;

public class Press : MonoBehaviour
{
    [SerializeField] private int _power;
    [SerializeField] private PressMoverForward _mover;
    [SerializeField] private float _hitDelay;
    [SerializeField] private int _energy;
    [SerializeField] private float _percentEnergyReducing;
    [SerializeField] private Wallet _wallet;

    private Part _currentPart;
    private float _passedTime;
    private float _currentEnergy;
    private float _energyReduced;

    public event Action<Part> PartDetected;
    public event Action EnergyEnded;
    public event Action<Part> PartHitted;

    private void OnValidate()
    {
        int maxEnergyReducingPercent = 1;

        if (_percentEnergyReducing >= maxEnergyReducingPercent)
            throw new ArgumentOutOfRangeException(nameof(_percentEnergyReducing));
    }

    private void Start()
    {
        ResetEnergy();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Part part))
        {
            if (part.Equals(_currentPart) == false)
            {
                _currentPart = part;
                PartDetected?.Invoke(part);
            }
        }
    }

    private void Update()
    {
        _passedTime += Time.deltaTime;

        if (_passedTime < _hitDelay)
            return;

        if (_currentPart != null)
            if (_currentPart.IsHittable && _mover.IsMove)
                HitPart();

        if (_currentEnergy <= 0)
            DisableEnergy();
    }

    private void HitPart()
    {
        _currentPart.TakeDamage(_power);
        _passedTime = 0;
        _wallet.AddMoney(_currentPart.Money);
        _currentEnergy -= _energyReduced;
    }

    private void DisableEnergy()
    {
        EnergyEnded?.Invoke();
        ResetEnergy();
        enabled = false;
    }

    private void ResetEnergy()
    {
        _energyReduced = _power * _percentEnergyReducing;
        _currentEnergy = _energy;
    }
}
