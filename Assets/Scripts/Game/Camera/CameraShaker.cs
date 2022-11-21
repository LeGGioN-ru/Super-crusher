using MilkShake;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private ShakePreset _presetBroken;
    [SerializeField] private ShakePreset _presetHit;
    [SerializeField] private Shaker _shaker;
    [SerializeField] private Press _press;
    [SerializeField] private GameRestarter _gameRestarter;

    private ShakeInstance _shakeInstance;

    private void OnEnable()
    {
        _gameRestarter.Restarting += OnStartRestarting;
        _press.PartDetected += OnPartDetected;
    }

    private void OnDisable()
    {
        _gameRestarter.Restarting -= OnStartRestarting;
        _press.PartDetected -= OnPartDetected;
    }

    private void OnPartDetected(Part part)
    {
        _shakeInstance = _shaker.Shake(_presetBroken);
        part.Destroyed += OnDestroyed;
    }

    private void OnDestroyed(Part part)
    {
        Shaker.ShakeAllSeparate(_presetHit);
        StopShake();
        part.Destroyed -= OnDestroyed;
    }

    private void OnStartRestarting()
    {
        StopShake();
    }

    private void StopShake()
    {
        _shakeInstance?.Stop(0, false);
    }
}
