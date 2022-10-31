using UnityEngine;

public class Item : MonoBehaviour
{
    private Rigidbody[] _rigidbodies;
    private Press _press;

    private void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void Init(Press press)
    {
        _press = press;
        press.EnergyEnded += OnEnergyEnded;
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
}
