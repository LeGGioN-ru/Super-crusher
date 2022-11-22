using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public class PressLimitPowerUp : MonoBehaviour
{
    [SerializeField] private float _durationTime;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private Press _press;
    [SerializeField] private int _powerUp;

    private WaitForSeconds _duration;

    public event Action PowerUpped;
    public event Action PowerUpEnded;

    private void Start()
    {
        _duration = new WaitForSeconds(_durationTime);
    }

    public void Execute()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif

        VideoAd.Show(null, StartPowering);
    }

    private void StartPowering()
    {
        StartCoroutine(PoweringUp());
    }

    private IEnumerator PoweringUp()
    {
        _press.SetAdditionalPower(_powerUp);
        PowerUpped?.Invoke();
        _particles.Play();

        yield return _duration;

        _particles.Stop();
        PowerUpEnded?.Invoke();
        _press.ResetAdditionalPower();
    }
}
