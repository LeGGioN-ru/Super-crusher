using UnityEngine;

public class Item : MonoBehaviour
{
    private Part[] _parts;
    private Rigidbody[] _rigidbodies;
    private Press _press;
    private GameRestarter _restarter;

    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _parts = GetComponentsInChildren<Part>();
    }

    public void Init(Press press, GameRestarter gameRestarter)
    {
        _press = press;
        _restarter = gameRestarter;
        press.EnergyEnded += OnEnergyEnded;

        foreach (var part in _parts)
            part.Destroyed += OnDestroyed;
    }

    private void OnDisable()
    {
        _press.EnergyEnded -= OnEnergyEnded;

        foreach (var part in _parts)
            part.Destroyed -= OnDestroyed;
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

        _press.EnergyEnded -= OnEnergyEnded;
    }

    private void OnDestroyed(Part part)
    {
        if (part.Equals(_parts[_parts.Length - 1]))
            StartCoroutine(_restarter.Execute());

    }
}
