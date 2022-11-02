using System;
using System.Collections;
using UnityEngine;

public class GameRestarter : MonoBehaviour
{
    [SerializeField] private Press _press;
    [SerializeField] private PressMoverForward _pressMoverForward;
    [SerializeField] private PressMoverBack _pressMoverBack;
    [SerializeField] private PressEnergy _pressEnergy;
    [SerializeField] private Animator _cleanerAnimator;
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private float _restartDelay;

    public event Action Restarted;

    private void OnEnable()
    {
        _pressMoverBack.Backed += OnBacked;
    }

    private void OnDisable()
    {
        _pressMoverBack.Backed -= OnBacked;
    }

    public void Execute()
    {
        _press.enabled = false;
        _pressMoverForward.enabled = false;
        _pressEnergy.enabled = false;

        StartCoroutine(Restrating());
    }

    private IEnumerator Restrating()
    {
        float passedTime = 0;

        while (passedTime < _restartDelay)
        {
            passedTime += Time.deltaTime;
            yield return null;
        }

        _pressMoverBack.Execute();
    }

    private void OnBacked()
    {
        _cleanerAnimator.Play(CleanerAnimationController.State.Clean);
    }

    private void EnablePress()
    {
        _press.enabled = true;
        _pressMoverForward.enabled = true;
        _pressEnergy.enabled = true;
        _spawner.Execute();
        Restarted?.Invoke();
    }
}
