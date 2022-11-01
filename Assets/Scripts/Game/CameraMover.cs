using DG.Tweening;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Press _press;
    [SerializeField] private PressEnergy _pressEnergy;
    [SerializeField] private Transform[] _cameraPositions;
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
        _press.PartDetected += OnPartDetected;
        _pressEnergy.EnergyEnded += OnEnergyEnded;
    }

    private void OnDisable()
    {
        _press.PartDetected -= OnPartDetected;
        _pressEnergy.EnergyEnded -= OnEnergyEnded;
    }

    private void OnPartDetected(Part part)
    {
        part.Destroyed += OnPartDestroyed;
    }

    private void OnPartDestroyed(Part part)
    {
        if (_currentCameraPositionIndex + 1 >= _cameraPositions.Length)
            _currentCameraPositionIndex = 0;
        else
            _currentCameraPositionIndex++;

        Execute();

        part.Destroyed -= OnPartDestroyed;
    }

    private void OnEnergyEnded()
    {
        _currentCameraPositionIndex = 0;
        Execute();
    }

    private void Execute()
    {
        _camera.transform.DOMove(_cameraPositions[_currentCameraPositionIndex].position, _moveDuration);
    }
}
