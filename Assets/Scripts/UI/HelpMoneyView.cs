using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpMoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textAddMoney;
    [SerializeField] private Animator _animator;
    [SerializeField] private HelpMoney _helpMoney;
    [SerializeField] private int _needNotDestroyItemsCount;
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private Button _buttonShow;
    [SerializeField] private Button _buttonClose;
    [SerializeField] private TMP_Text _textButtonClose;
    [SerializeField] private int _timeBlockButton;

    private int _notDestroyedItemsCount = 0;
    private bool _isSpawned = false;

    private void OnEnable()
    {
        _buttonClose.onClick.AddListener(OnCloseButtonClick);
        _buttonShow.onClick.AddListener(OnShowButtonClick);
        _spawner.Spawned += OnSpawned;
    }

    private void OnDisable()
    {
        _buttonClose.onClick.RemoveListener(OnCloseButtonClick);
        _buttonShow.onClick.RemoveListener(OnShowButtonClick);
        _spawner.Spawned -= OnSpawned;
    }

    private void OnShowButtonClick()
    {
        _helpMoney.ShowAd();
    }

    private void OnCloseButtonClick()
    {
        _animator.Play("Hide");
    }

    private void OnSpawned(Item item)
    {
        item.Destroyed += OnDestroyed;

        if (_isSpawned == false)
        {
            _isSpawned = true;
            return;
        }

        _isSpawned = false;
        _notDestroyedItemsCount = 0;
    }

    private void OnDestroyed(Item item)
    {
        _notDestroyedItemsCount++;

        if (_needNotDestroyItemsCount <= _notDestroyedItemsCount)
            _animator.Play("Show");

        _isSpawned = false;
        item.Destroyed -= OnDestroyed;
    }

    private void ResetHelpMoney()
    {
        _textAddMoney.text = _helpMoney.HelpMoneyAdd.ToString();
        _textButtonClose.text = _timeBlockButton.ToString();
        _buttonClose.interactable = false;
        StartCoroutine(BlockButton());
    }

    private IEnumerator BlockButton()
    {
        float passedTime = 0;

        while (passedTime < _timeBlockButton)
        {
            passedTime += Time.deltaTime;
            _textButtonClose.text = Convert.ToInt32(passedTime).ToString();
            yield return null;
        }

        _buttonClose.interactable = true;
    }
}

