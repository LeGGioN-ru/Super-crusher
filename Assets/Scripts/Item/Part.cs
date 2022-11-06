using System;
using System.Collections.Generic;
using RayFire;
using UnityEngine;

public class Part : MonoBehaviour
{
    [SerializeField] private List<ConstituentPart> _constituentParts;

    private PartStats _stats;

    public event Action<Part> Destroyed;

    public bool IsHittable => _stats.Durability > 0;
    public int Money => _stats.Money;

    public void SetStats(PartStats partStats)
    {
        _stats = new PartStats(partStats);
    }

    public float TakeDamage(int damage)
    {
        _stats.ReduceDurability(damage);
        

        if (IsHittable == false)
        {
            Destroyed?.Invoke(this);
            DemolishRigid();
        }

        return Math.Clamp(Convert.ToSingle(damage) / _stats.MaxDurability, 0, 1);
    }

    public void MoveConstitientsParts()
    {
        _constituentParts.ForEach(part => part.Move());
    }

    private void DemolishRigid()
    {
        if (TryGetComponent(out RayfireRigid rigid))
            rigid.Demolish();
    }
}
