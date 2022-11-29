using System;
using System.Collections;
using UnityEngine;

public class PressLimitPowerUp : MonoBehaviour
{
    [SerializeField] private float _durationTime;
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private Press _press;
    [SerializeField] private int _powerUp;

    private WaitForSeconds _duration;
    private VideoAdvertisingShower _videoAdvertisingShower;

    public event Action PowerUpped;
    public event Action PowerUpEnded;

    private void Start()
    {
        _videoAdvertisingShower = new VideoAdvertisingShower();
        _duration = new WaitForSeconds(_durationTime);
    }

    public void Execute()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        StartPowering();
        return;
#endif
        _videoAdvertisingShower.Execute(StartPowering);
    }

    private void StartPowering()
    {
        StartCoroutine(PoweringUp());
    }

    private IEnumerator PoweringUp()
    {
        _press.SetAdditionalPower(_powerUp);
        PowerUpped?.Invoke();

        foreach (var particle in _particles)
            particle.Play();

        yield return _duration;

        foreach (var particle in _particles)
            particle.Stop();

        PowerUpEnded?.Invoke();
        _press.ResetAdditionalPower();
    }
}
