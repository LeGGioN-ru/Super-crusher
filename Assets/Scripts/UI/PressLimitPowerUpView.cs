using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PressLimitPowerUp))]
public class PressLimitPowerUpView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private PressLimitPowerUp _limitPowerUp;
    [SerializeField] private float _powerUpLockDuration;

    private WaitForSeconds _lockDuration;

    private void Awake()
    {
        _lockDuration = new WaitForSeconds(_powerUpLockDuration);
        ResetTimePowerUp();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
        _limitPowerUp.PowerUpped += OnPowerUpped;
        _limitPowerUp.PowerUpEnded += OnPowerUpEnded;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClicked);
        _limitPowerUp.PowerUpped -= OnPowerUpped;
        _limitPowerUp.PowerUpEnded -= OnPowerUpEnded;
    }

    private void OnClicked()
    {
        _limitPowerUp.Execute();
    }

    private void OnPowerUpEnded()
    {
        StartCoroutine(ShowButton());
    }

    private void OnPowerUpped()
    {
        _image.enabled = false;
        _button.enabled = false;
    }

    private IEnumerator ShowButton()
    {
        yield return _lockDuration;

        _image.enabled = true;
        _button.enabled = true;
    }

    private void ResetTimePowerUp()
    {
        _image.enabled = false;
        _button.enabled = false;
        StartCoroutine(ShowButton());
    }
}

