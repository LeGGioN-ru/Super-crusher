using System;
using System.Collections.Generic;
using RayFire;
using UnityEngine;

public class Part : MonoBehaviour
{
    [SerializeField] private int _powerThreashold;
    [SerializeField] private int _health;
    [SerializeField] private List<ConstituentPart> _constituentParts;

    public event Action<Part> Destroyed;

    public int PowerThreashold => _powerThreashold;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _constituentParts.ForEach(part => part.Move());

        if (_health <= 0)
        {
            Destroyed?.Invoke(this);
            DemolishRigid();
        }
    }

    private void DemolishRigid()
    {
        if (TryGetComponent(out RayfireRigid rigid))
            rigid.Demolish();
    }
}
