using System.Collections;
using UnityEngine;

public class GameRestarter : MonoBehaviour
{
    [SerializeField] private Press _press;
    [SerializeField] private PressMoverForward _pressMoverForward;
    [SerializeField] private PressMoverBack _pressMoverBack;
    [SerializeField] private Animator _cleanerAnimator;
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private float _restartDelay;

    private void OnEnable()
    {
        _press.EnergyEnded += OnEnergyEnded;
        _pressMoverBack.Backed += OnBacked;
    }

    private void OnDisable()
    {
        _press.EnergyEnded -= OnEnergyEnded;
        _pressMoverBack.Backed -= OnBacked;
    }

    private void OnEnergyEnded()
    {
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
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
        _spawner.Execute();
    }
}
