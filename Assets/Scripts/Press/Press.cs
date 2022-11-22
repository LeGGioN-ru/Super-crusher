using System;
using UnityEngine;

public class Press : MonoBehaviour
{
    [SerializeField] private int _startPower;
    [SerializeField] private PressMoverForward _mover;
    [SerializeField] private float _hitDelay;
    [SerializeField] private Wallet _wallet;

    private Part _currentPart;
    private float _passedTime;
    private int _currentPower;
    private int _additionalPower;

    public int StartPower => _startPower;
    public int CurrentPower => _currentPower;

    public event Action<Part> PartDetected;
    public event Action PartHitted;
    public event Action PowerSetted;

    private void Start()
    {
        _currentPower = _startPower;
    }

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

        if (_currentPart == null)
            return;

        if (_currentPart.IsHittable == false || _mover.IsMove == false)
            return;

        _currentPart.MoveConstitientsParts();

        if (_passedTime < _hitDelay)
            return;

        _passedTime = 0;
        HitPart();
    }

    public void SetPower(int power)
    {
        if (power <= 0)
            throw new ArgumentException(nameof(power));

        _currentPower = power;
        PowerSetted?.Invoke();
    }

    public void UpgradePower(int powerUp)
    {
        _currentPower += powerUp;
    }

    public void SetAdditionalPower(int additionalPower)
    {
        if (additionalPower <= 0)
            throw new ArgumentException(nameof(additionalPower));

        _additionalPower = additionalPower;
    }

    public void ResetAdditionalPower()
    {
        _additionalPower = 0;
    }

    private void HitPart()
    {
        float percentItemDamage = _currentPart.TakeDamage(_currentPower + _additionalPower);
        _wallet.AddMoney(Convert.ToInt32(_currentPart.Money * percentItemDamage));
        PartHitted?.Invoke();
    }
}