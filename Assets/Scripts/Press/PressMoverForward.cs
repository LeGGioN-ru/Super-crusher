using System;
using UnityEngine;

[RequireComponent(typeof(Press))]
public class PressMoverForward : MonoBehaviour
{
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _slowedSpeed;

    private Press _press;
    private PlayerInput _playerInput;
    private float _currentSpeed;
    private bool _isMove;
    private Vector3 _downDirection = new Vector3(0, 0, -1);

    public event Action Moved;

    public bool IsMove => _isMove;

    private void Awake()
    {
        _currentSpeed = _defaultSpeed;

        _playerInput = new PlayerInput();

        _playerInput.Player.PressMove.performed += ctx => OnPressMoved();
        _playerInput.Player.PressMove.canceled += ctx => OnPressMoveCanceled();

        _press = GetComponent<Press>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        _press.PartDetected += OnPartDetected;

        _currentSpeed = _defaultSpeed;
    }

    private void OnDisable()
    {
        _playerInput.Disable();

        _press.PartDetected -= OnPartDetected;
    }

    private void Update()
    {
        if (_isMove)
            transform.Translate(_currentSpeed * Time.deltaTime * _downDirection);
    }

    private void OnPressMoved()
    {
        Moved?.Invoke();
        _isMove = true;
    }

    private void OnPressMoveCanceled()
    {
        _isMove = false;
    }

    private void OnPartDetected(Part part)
    {
        _currentSpeed = _slowedSpeed;
        part.Destroyed += OnPartDestroyed;
    }

    private void OnPartDestroyed(Part part)
    {
        _currentSpeed = _defaultSpeed;
        part.Destroyed -= OnPartDestroyed;
    }
}
