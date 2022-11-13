using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private PressMoverForward _mover;
    [SerializeField] private Button _otherPanelOpenButton;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

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
        _otherPanelOpenButton.interactable = true;
        _openButton.interactable = true;
        _mover.enabled = true;
        _animator.Play(ShopAnimationController.State.Hide);
    }

    private void OnOpenButtonClick()
    {
        _otherPanelOpenButton.interactable = false;
        _openButton.interactable = false;
        _mover.enabled = false;
        _animator.Play(ShopAnimationController.State.Show);
    }
}
