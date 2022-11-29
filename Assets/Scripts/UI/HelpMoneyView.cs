using Lean.Localization;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HelpMoney))]
public class HelpMoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textAddMoney;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _needNotDestroyItemsCount;
    [SerializeField] private GameRestarter _restarter;
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private Button _buttonShow;
    [SerializeField] private Button _buttonClose;
    [SerializeField] private TMP_Text _textButtonClose;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _textButtonCloseLocalization;
    [SerializeField] private string _exitButtonLocalization;
    [SerializeField] private int _timeBlockButton;
    [SerializeField] private PressMoverForward _mover;

    private HelpMoney _helpMoney;
    private int _notDestroyedItemsCount = 0;

    private void Awake()
    {
        _helpMoney = GetComponent<HelpMoney>();
    }

    private void OnEnable()
    {
        _buttonClose.onClick.AddListener(OnCloseButtonClick);
        _buttonShow.onClick.AddListener(OnShowButtonClick);
        _spawner.Spawned += OnSpawned;
        _restarter.Restarted += OnRestarted;
    }

    private void OnDisable()
    {
        _buttonClose.onClick.RemoveListener(OnCloseButtonClick);
        _buttonShow.onClick.RemoveListener(OnShowButtonClick);
        _spawner.Spawned -= OnSpawned;
        _restarter.Restarted -= OnRestarted;
    }

    private void OnShowButtonClick()
    {
        _helpMoney.ShowAd();
        _animator.Play(HelpMoneyAnimationController.State.Hide);
    }

    private void OnCloseButtonClick()
    {
        _mover.enabled = true;
        _notDestroyedItemsCount = 0;
        _animator.Play(HelpMoneyAnimationController.State.Hide);
    }

    private void OnRestarted()
    {
        if (_needNotDestroyItemsCount <= _notDestroyedItemsCount)
        {
            _animator.Play(HelpMoneyAnimationController.State.Show);
            _mover.enabled = false;
        }
    }

    private void OnSpawned(Item item)
    {
        item.Destroyed += OnDestroyed;
        _notDestroyedItemsCount++;
    }

    private void OnDestroyed(Item item)
    {
        _notDestroyedItemsCount = 0;

        item.Destroyed -= OnDestroyed;
    }

    private void ResetHelpMoney()
    {
        _textAddMoney.text = NumberCuter.Execute(_helpMoney.HelpMoneyAdd);
        _textButtonClose.text = _timeBlockButton.ToString();
        _buttonClose.interactable = false;
        StartCoroutine(BlockButton());
    }

    private IEnumerator BlockButton()
    {
        int updateDelay = 1;
        int passedSeconds = 0;

        WaitForSeconds waitForSeconds = new WaitForSeconds(updateDelay);

        while (passedSeconds < _timeBlockButton)
        {
            passedSeconds++;
            _textButtonClose.text = (Math.Abs(passedSeconds - _timeBlockButton) + 1).ToString();
            yield return waitForSeconds;
        }

        _textButtonClose.text = _exitButtonLocalization;
        _textButtonCloseLocalization.UpdateTranslation(new LeanTranslation(_exitButtonLocalization));
        _buttonClose.interactable = true;
    }
}

