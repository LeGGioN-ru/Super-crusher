using System;
using UnityEngine;

public class Press : MonoBehaviour
{
    [SerializeField] private int _power;
    [SerializeField] private PressMoverForward _mover;
    [SerializeField] private float _hitDelay;
    [SerializeField] private Wallet _wallet;

    private Part _currentPart;
    private float _passedTime;

    public int Power => _power;

    public event Action<Part> PartDetected;
    public event Action PartHitted;

    private void OnEnable()
    {
        _currentPart = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Part part))
        {
            if (_currentPart == null)
            {
                _currentPart = part;
                PartDetected?.Invoke(part);
            }
        }
    }

    private void Update()
    {
        _passedTime += Time.deltaTime;

        if (_passedTime < _hitDelay)
            return;

        if (_currentPart != null)
        {
            if (_currentPart.IsHittable && _mover.IsMove)
            {
                _passedTime = 0;
                HitPart();
            }
        }
    }

    public void UpgradePower(int powerUp)
    {
        _power += powerUp;
    }

    private void HitPart()
    {
        if (_currentPart == null)
            return;

        float percentItemDamage = _currentPart.TakeDamage(_power);
        _wallet.AddMoney(Convert.ToInt32(_currentPart.Money * percentItemDamage));
        PartHitted?.Invoke();
    }
}