using System;
using System.Collections.Generic;
using RayFire;
using UnityEngine;

public class Part : MonoBehaviour
{
    [SerializeField] private int _money;
    [SerializeField] private int _health;
    [SerializeField] private List<ConstituentPart> _constituentParts;

    public event Action<Part> Destroyed;

    public bool IsHittable => _health >= 0;
    public int Money => _money;

    public void TakeDamage(int damage)
    {
        if (_health <= 0)
        {
            Destroyed?.Invoke(this);
            DemolishRigid();
            return;
        }

        _health -= damage;
        _constituentParts.ForEach(part => part.Move());
    }

    private void DemolishRigid()
    {
        if (TryGetComponent(out RayfireRigid rigid))
            rigid.Demolish();
    }
}
