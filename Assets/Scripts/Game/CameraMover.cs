using DG.Tweening;
using System;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Press _press;
    [SerializeField] private PressEnergy _pressEnergy;
    [SerializeField] private PressMoverForward _moverForward;
    [SerializeField] private Transform[] _cameraPositions;
    [SerializeField] private GameRestarter _restarter;
    [SerializeField] private float _moveDuration;

    private Camera _camera;
    private int _currentCameraPositionIndex;

    private void Awake()
    {
        _camera = Camera.main;
        _camera.transform.position = _cameraPositions[_currentCameraPositionIndex].position;
    }

    private void OnEnable()
    {
        _moverForward.Moved += OnPressMoved;
        _press.PartDetected += OnPartDetected;
        _restarter.Restarted += OnRestarted;
    }

    private void OnDisable()
    {
        _moverForward.Moved -= OnPressMoved;
        _press.PartDetected -= OnPartDetected;
        _restarter.Restarted -= OnRestarted;
    }

    private void OnRestarted()
    {
        _currentCameraPositionIndex = 0;
        Execute();
    }

    private void OnPartDetected(Part part)
    {
        part.Destroyed += OnPartDestroyed;
    }

    private void OnPartDestroyed(Part part)
    {
        if (_currentCameraPositionIndex + 1 >= _cameraPositions.Length)
            return;

        _currentCameraPositionIndex++;
        Execute();

        part.Destroyed -= OnPartDestroyed;
    }

    private void Execute()
    {
        _camera.transform.DOMove(_cameraPositions[_currentCameraPositionIndex].position, _moveDuration);
    }

    private void OnPressMoved()
    {
        if (_currentCameraPositionIndex > 0)
            return;

        _currentCameraPositionIndex++;
        Execute();
    }
}
