using Lean.Localization;
using System;
using TMPro;
using UnityEngine;

public class Education : MonoBehaviour
{
    [SerializeField] private Animator _animatorPanel;
    [SerializeField] private PressMoverForward _pressMover;
    [SerializeField] private GameRestarter _gameRestarted;
    [SerializeField] private ItemUp _itemUp;
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private Press _press;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _message;
    [SerializeField] private string _afterFailBreakItem;
    [SerializeField] private string _afterSuccesBreakItem;
    [SerializeField] private string _afterUpgradeItem;

    private bool _isDone;

    public bool IsDone => _isDone;

    private void OnEnable()
    {
        _spawner.Spawned += OnSpawned;
        _pressMover.Moved += OnMoved;
        _gameRestarted.Restarted += Restarted;
        _itemUp.Executed += OnUpgraded;
        _press.PartDetected += OnPartDetected;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= OnSpawned;
        _pressMover.Moved -= OnMoved;
        _gameRestarted.Restarted -= Restarted;
        _itemUp.Executed -= OnUpgraded;
        _press.PartDetected -= OnPartDetected;
    }

    public void CountEducation()
    {
        _isDone = true;
    }

    private void OnSpawned(Item item)
    {
        item.Destroyed += OnDestroyed;
    }

    private void OnDestroyed(Item item)
    {
        _message.TranslationName = _afterSuccesBreakItem;

        item.Destroyed -= OnDestroyed;
        _spawner.Spawned -= OnSpawned;
    }

    private void OnPartDetected(Part part)
    {
        _message.TranslationName = _afterFailBreakItem;
        _press.PartDetected -= OnPartDetected;
    }

    private void OnUpgraded()
    {
        _message.TranslationName = _afterUpgradeItem;
        _isDone = true;
        _itemUp.Executed -= OnUpgraded;
    }

    private void Restarted()
    {
        _animatorPanel.Play(EduactionPanelAnimationController.State.Show);
    }

    private void OnMoved()
    {
        _animatorPanel.Play(EduactionPanelAnimationController.State.Hide);
    }

    private void Disable()
    {
        if (_isDone)
            gameObject.SetActive(false);
    }
}
