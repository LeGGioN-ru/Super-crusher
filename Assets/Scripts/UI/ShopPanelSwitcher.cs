using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelSwitcher : MonoBehaviour
{
    [SerializeField] private Image _panel;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private PressMoverForward _mover;
    [SerializeField] private UIVisibleSwitcher _visibleSwitcher;

    private void OnEnable()
    {
        _openButton.onClick.AddListener(OnOpenButtonClick);
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(OnOpenButtonClick);
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        _mover.enabled = true;
        _closeButton.interactable = false;
        _openButton.interactable = true;
        _panel.gameObject.SetActive(false);
    }

    private void OnOpenButtonClick()
    {
        _mover.enabled = false;
        _openButton.interactable = false;
        _closeButton.interactable = true;
        _panel.gameObject.SetActive(true);
    }
}
