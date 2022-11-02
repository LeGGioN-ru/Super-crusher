using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHider : MonoBehaviour
{
    [SerializeField] private PressMoverForward _moverForward;
    [SerializeField] private CanvasGroup _upgrades;
    [SerializeField] private GameRestarter _gameRestarter;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.PressMove.performed += ctx => OnPressMove();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        _gameRestarter.Restarted += OnRestarted;
    }

    private void OnDisable()
    {
        _playerInput.Disable();

        _gameRestarter.Restarted -= OnRestarted;
    }

    private void OnPressMove()
    {
        _upgrades.blocksRaycasts = false;
        _upgrades.alpha = 0;
    }

    private void OnRestarted()
    {
        _upgrades.blocksRaycasts = true;
        _upgrades.alpha = 1;
    }
}
