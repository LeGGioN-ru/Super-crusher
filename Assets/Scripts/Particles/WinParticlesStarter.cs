using System;
using UnityEngine;

public class WinParticlesStarter : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private ItemSpawner _spawner;

    private void OnEnable()
    {
        _spawner.Spawned += OnSpawned;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= OnSpawned;
    }

    private void OnSpawned(Item item)
    {
        item.Destroyed += Execute;
    }

    private void Execute(Item item)
    {
        foreach (ParticleSystem particle in _particles)
            particle.Play();

        item.Destroyed -= Execute;
    }
}
