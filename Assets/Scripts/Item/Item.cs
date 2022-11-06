using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Part[] _parts;
    private Rigidbody[] _rigidbodies;
    private PressEnergy _pressEnergy;
    private GameRestarter _restarter;

    private float _destroyDelay = 10;

    public event Action<Item> Destroyed;

    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _parts = GetComponentsInChildren<Part>();
    }

    public void Init(PressEnergy pressEnergy, GameRestarter gameRestarter, PartStats partStats)
    {
        _pressEnergy = pressEnergy;
        _restarter = gameRestarter;
        _pressEnergy.EnergyEnded += OnEnergyEnded;

        PartStats definedPartStats = new PartStats(partStats.Money, partStats.Durability / _parts.Length);

        foreach (var part in _parts)
        {
            part.Destroyed += OnDestroyed;
            part.SetStats(definedPartStats);
        }
    }

    private void OnDisable()
    {
        _pressEnergy.EnergyEnded -= OnEnergyEnded;

        foreach (var part in _parts)
        {
            part.Destroyed -= OnDestroyed;
        }
    }

    private void OnEnergyEnded()
    {
        if (_rigidbodies.Length == 0)
            return;

        foreach (var rigidbody in _rigidbodies)
        {
            if (rigidbody != null)
                rigidbody.constraints = RigidbodyConstraints.None;
        }

        StartCoroutine(Destroying());
    }

    private void OnDestroyed(Part part)
    {
        if (part.Equals(_parts[_parts.Length - 1]))
        {
            Destroyed?.Invoke(this);
            _restarter.Execute();
            StartCoroutine(Destroying());
        }
    }

    private IEnumerator Destroying()
    {
        yield return new WaitForSeconds(_destroyDelay);

        Destroy(gameObject);
    }
}
