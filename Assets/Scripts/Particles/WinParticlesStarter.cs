using UnityEngine;

public class WinParticlesStarter : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private SoundSettings _soundSettings;

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

        _soundSettings.PlaySound();

        item.Destroyed -= Execute;
    }
}